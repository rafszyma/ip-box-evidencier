using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace IpBoxEvidencier;

public class KPiRReader
{
    private const string InputDirectory = "E:\\CodeHome\\Payload\\Input";

    private const string Year = "2024";

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
        if (kpir == null)
        {
            Console.WriteLine($"No file found for: {month.Name}");
            return;
        }
        
        ParsePdf(kpir, month);
    }

    private void ParsePdf(string fileName, OutputMonth month)
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
                var end = Array.IndexOf(formatted, "Suma w miesiącu:") - 1;
                var payload = formatted
                    .Skip(start + 1)
                    .Take(end - start - 1)
                    .ToList();

                if (payload.Count % 2 != 0)
                {
                    throw new ArgumentException("Invalid number of records");
                }

                for (var i = 0; i < payload.Count; i = i + 2)
                {
                    var entry = new Entry(payload[i], payload[i + 1]);
                    month.Entries.Add(entry.ToOutput());
                }
            }
        }
    }
}