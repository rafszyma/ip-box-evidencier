namespace IpBoxEvidencier;

public class OutputMonth
{
    public string Name { get; set; }
    
    public int Id { get; set; }
    
    public IList<Output> Entries { get; set; }
}