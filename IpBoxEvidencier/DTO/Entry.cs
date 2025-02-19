namespace IpBoxEvidencier;

public class Entry
{
    public string KPiRName { get; set; }
    
    public double Income { get; set; }
    
    public double Expend { get; set; }
    
    public DateTime Date { get; set; }

    public Output ToOutput()
    {
        // TODO implement it
        return new Output();
    }
}