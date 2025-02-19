namespace IpBoxEvidencier;

public class OutputMonth
{
    public OutputMonth(int id)
    {
        switch (id)
        {
            case 1:
                Id = "01";
                Name = "Styczeń"; // January
                break;
            case 2:
                Id = "02";
                Name = "Luty"; // February
                break;
            case 3:
                Id = "03";
                Name = "Marzec"; // March
                break;
            case 4:
                Id = "04";
                Name = "Kwiecień"; // April
                break;
            case 5:
                Id = "05";
                Name = "Maj"; // May
                break;
            case 6:
                Id = "06";
                Name = "Czerwiec"; // June
                break;
            case 7:
                Id = "07";
                Name = "Lipiec"; // July
                break;
            case 8:
                Id = "08";
                Name = "Sierpień"; // August
                break;
            case 9:
                Id = "09";
                Name = "Wrzesień"; // September
                break;
            case 10:
                Id = "10";
                Name = "Październik"; // October
                break;
            case 11:
                Id = "11";
                Name = "Listopad"; // November
                break;
            case 12:
                Id = "12";
                Name = "Grudzień"; // December
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid month ID");
        }

    }
    public string Name { get; set; }
    
    public string Id { get; set; }

    public IList<Output> Entries { get; set; } = new List<Output>();

    public static List<OutputMonth> Create()
    {
        return new List<OutputMonth>
        {
            new (1),
            new (2),
            new (3),
            new (4),
            new (5),
            new (6),
            new (7),
            new (8),
            new (9),
            new (10),
            new (11),
            new (12),
        };
    }
}