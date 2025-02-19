using System.Globalization;
using DocumentFormat.OpenXml.Wordprocessing;

namespace IpBoxEvidencier;

public class Entry
{
    public string KPiRName { get; set; }
    
    public double? Income { get; set; }
    
    public double? Expend { get; set; }
    
    public DateTime Date { get; set; }


    public Entry(params string[] input)
    {
        if (input.Length != 2)
        {
            throw new ArgumentException("Invalid Entry input");
        }
        
        SetName(input[0]);
        SetValues(input[1]);
        
    }
    
    public Output ToOutput()
    {
        return new Output
        {
            Name = KPiRName,
            Income = Income,
            Expend = Expend
        };
    }

    private void SetName(string input)
    {
        string[] parts = input.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 3)
        {
            throw new ArgumentException("Invalid name format");
        }
        
        if (DateTime.TryParseExact(parts[1], "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            Date = parsedDate;
        }
        else
        {
            throw new ArgumentException("Invalid date format");
        }
        
        KPiRName = parts[2];
    }
    
    private void SetValues(string input)
    {
        var cleanedInput = input.Replace(" ", "");
        
        string[] parts = cleanedInput.Split(',');

        var numbersAsString = new List<string>();

        string wholePart = parts[0];

        for (int i = 1; i < parts.Length; i++)
        {
            if (parts[i].Length < 2) throw new ArgumentException("Not enough digits");
            numbersAsString.Add($"{wholePart}.{parts[i][..2]}");
            wholePart = parts[i][2..];
        }
        
        var numbers = numbersAsString.Select(x => double.Parse(x, CultureInfo.InvariantCulture)).ToList();

        if (numbers[0] > 0 && numbers[1] > 0)
        {
            throw new NotImplementedException("Not implemented income");
        }
        
        if (numbers[0] + numbers[1] > numbers[2])
        {
            throw new ArgumentException("Wrong income");
        }
        
        if (numbers[3] + numbers[4] > 0)
        {
            throw new NotImplementedException("Not implemented fields");
        }

        if (numbers[5] > 0 && numbers[6] > 0)
        {
            throw new NotImplementedException("Not implemented income");
        }
        
        if (numbers[5] + numbers[6] > numbers[7])
        {
            throw new ArgumentException("Wrong expend");
        }
        
        if (numbers[8] > 0)
        {
            throw new NotImplementedException("Not used field?");
        }
        
        Income = numbers[2];
        Expend = numbers[7];

    }
}