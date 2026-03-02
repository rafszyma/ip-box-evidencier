using System.Text.Json;

namespace IpBoxEvidencier;

public class Weight
{
    public string? Contains { get; set; }

    public string? NotContains { get; set; }
    
    public string Name { get; set; }

    public double IpValue { get; set; }
    
    public double RegularValue { get; set; }
    
    public double MinValue { get; set; }
}

public class Weighter
{
    public Weighter()
    {
        var file = File.ReadAllText("weights.json");
        _weights = JsonSerializer.Deserialize<Weight[]>(file) ?? [];
    }
    
    private readonly Weight[] _weights;
    public Output Transform(Entry entry)
    {
        var weights = _weights.Where(x =>
            (x.Contains == null || entry.KPiRName.Contains(x.Contains, StringComparison.InvariantCultureIgnoreCase)) &&
            (x.NotContains == null || !entry.KPiRName.Contains(x.NotContains, StringComparison.InvariantCultureIgnoreCase))
        ).ToList();
        
        
        if (weights.Count() > 1)
        {
            // If more than 1 weight matches, filter by MinValue
            var tempWeights = weights.Where(x => x.MinValue > 0 && entry.Income > x.MinValue).ToList();
            if (tempWeights.Count == 0)
            {
                tempWeights = weights.Where(x => x.MinValue == 0).ToList();
            }

            weights = tempWeights;
            
            if (weights.Count > 1)
            {
                Console.WriteLine($"Multiple weights for entry: {entry.KPiRName} in {entry.Date}");
                throw new ArgumentException();
            }
        }
        
        var weight = weights.SingleOrDefault();
        if (weight == null)
        {
            Console.WriteLine($"No weight for entry: {entry.KPiRName} in {entry.Date}");
            Console.ReadKey();
            return new Output();
        }
        
        var full = weight.IpValue + weight.RegularValue;
        return new Output
        {
            Entry = entry,
            Name = weight.Name,
            Expend = entry.Expend * weight.RegularValue / full,
            IPExpend = entry.Expend * weight.IpValue / full,
            Income = entry.Income * weight.RegularValue / full,
            IPIncome = entry.Income * weight.IpValue / full
        };
    }
}