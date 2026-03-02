using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace IpBoxEvidencier;

public class KPiRReader
{
    private const string InputDirectory = "D:\\CodeHome\\Payload\\Input";

    private const string Year = "2025";

    private readonly Weighter _weighter;
    public KPiRReader(Weighter weighter)
    {
        _weighter = weighter;
    }

    public List<OutputMonth> Read()
    {
        var months = OutputMonth.Create();

        foreach (var month in months)
        {
            ReadFile(month);
        }

        return months;
    }

    private void ReadFile(OutputMonth month)
    {
        var files = Directory.GetFiles(InputDirectory);
        var kpir = files.FirstOrDefault(x => x.Contains($"{month.Id}_{Year}"));
        if (kpir != null)
        {
            ParsePdfV1(kpir, month);
            return;
        }
        
        kpir = files.FirstOrDefault(x => x.Contains($"{month.Id}{Year[2..]}"));
        if (kpir != null)
        {
            ParsePdfV2(kpir, month);
            return;
        }

        if (kpir == null)
        {
            throw new ArgumentException($"No KPiR file for {month.Id} {Year}");
        }
    }

    private void ParsePdfV1(string fileName, OutputMonth month)
    {
        var file = File.OpenRead(fileName);
        using (var reader = new StreamReader(file))
        {
            using (var pdf = PdfDocument.Open(reader.BaseStream))
            {
                var pages = pdf.GetPages();
                if (pages.Count() > 1)
                {
                    throw new NotImplementedException("We just support 1 page");
                } 
                
                var text = ContentOrderTextExtractor.GetText(pages.First());
                var formatted = text.Split(Environment.NewLine);
                var start = Array.IndexOf(formatted, "1");
                var end = Array.FindIndex(formatted, x => x.Contains("Suma w miesiącu")) - 1;
                var payload = formatted
                    .Skip(start + 1)
                    .Take(end - start - 1)
                    .ToList();

                if (payload.Count == 0 || payload.Count % 2 != 0)
                {
                    throw new ArgumentException("Invalid number of records");
                }

                for (var i = 0; i < payload.Count; i = i + 2)
                {
                    var entry = new Entry(payload[i], payload[i + 1]);
                    var output = _weighter.Transform(entry);
                    month.Entries.Add(output);
                }
            }
        }
    }
    
    private void ParsePdfV2(string fileName, OutputMonth month)
    {
        var file = File.OpenRead(fileName);
        using (var reader = new StreamReader(file))
        {
            using (var pdf = PdfDocument.Open(reader.BaseStream))
            {
                var pages = pdf.GetPages();
                if (pages.Count() > 1)
                {
                    throw new NotImplementedException("We just support 1 page");
                } 
                
                var text = ContentOrderTextExtractor.GetText(pages.First());
                var formatted = text.Split(Environment.NewLine);
                var start = Array.IndexOf(formatted, "Rafał Szymala  80-330 GDAŃSK Gdańsk, ul. Opacka 19/31");
                var end = Array.FindIndex(formatted, x => x.Contains("Suma w miesiącu")) - 1;
                var payload = formatted
                    .Skip(start + 1)
                    .Take(end - start)
                    .ToList();

                if (payload.Count == 0 || payload.Count % 2 != 0)
                {
                    throw new ArgumentException("Invalid number of records");
                }

                for (var i = 0; i < payload.Count; i = i + 2)
                {
                    var entry = new Entry(payload[i], payload[i + 1]);
                    var output = _weighter.Transform(entry);
                    month.Entries.Add(output);
                }
            }
        }
    }
}