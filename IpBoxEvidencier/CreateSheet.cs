using ClosedXML.Excel;
namespace IpBoxEvidencier;

public class CreateSheet
{
    private const double RegularColumn = 24.71;

    private const double WideColumn = 43.57;
    
    private const string Output = "E:\\CodeHome\\Payload\\excel.xlsx";

    private const XLBorderStyleValues BorderThickness = XLBorderStyleValues.Medium;

    private XLColor HeaderColor = XLColor.FromHtml("#c6e0b4");
    
    private IXLWorksheet _worksheet;

    private IXLWorkbook _workbook;

    private int currentRow = 1;
    
    private int currentColumn = 1;

    public CreateSheet()
    {
        if (File.Exists(Output))
        {
            File.Delete(Output);
        }
        
        _workbook = new XLWorkbook();
        _worksheet = _workbook.Worksheets.Add();
        _worksheet.Name = (DateTime.UtcNow.Year - 1).ToString();
    }

    public void CreateExcel(List<OutputMonth> months)
    {
        SetHeaders();

        foreach (var month in months)
        {
            if (month.Entries.Any())
            {
                SetMonth(month);
            }
        }
        
        FormatStartOfTheMonthCell();
        
        _worksheet.SheetView.ZoomScale = 130;
        _workbook.SaveAs(Output);
    }

    private void SetMonth(OutputMonth month)
    {
        FormatStartOfTheMonthCell();
        _worksheet.Cell(currentRow, 1).Value = month.Name;
        foreach (var entry in month.Entries)
        {
            SetEntry(entry);
        }
    }

    private void SetHeaders()
    {
        _worksheet.Column(1).Width = RegularColumn;
        _worksheet.Column(2).Width = WideColumn;
        _worksheet.Column(3).Width = RegularColumn;
        _worksheet.Column(4).Width = RegularColumn;
        _worksheet.Column(5).Width = RegularColumn;
        _worksheet.Column(6).Width = RegularColumn;
        _worksheet.Column(7).Width = RegularColumn;
        _worksheet.Cell(currentRow, currentColumn).Value = "Miesiąc";
        FormatHeaderCell();
        _worksheet.Cell(currentRow, currentColumn).Value = "Nazwa";
        FormatHeaderCell();
        _worksheet.Cell(currentRow, currentColumn).Value = "Wydatki";
        FormatHeaderCell();
        _worksheet.Cell(currentRow, currentColumn).Value = "Wydatki IP";
        FormatHeaderCell();
        _worksheet.Cell(currentRow, currentColumn).Value = "Przychód";
        FormatHeaderCell();
        _worksheet.Cell(currentRow, currentColumn).Value = "Przychód IP";
        FormatHeaderCell();
        _worksheet.Cell(currentRow, currentColumn).Value = "Uwagi";
        FormatHeaderCell();
        currentColumn = 1;
        currentRow++;
    }

    private void FormatHeaderCell()
    {
        _worksheet.Cell(currentRow, currentColumn).Style.Font.Bold = true;
        _worksheet.Cell(currentRow, currentColumn).Style.Border.BottomBorder = BorderThickness;
        _worksheet.Cell(currentRow, currentColumn).Style.Border.RightBorder = BorderThickness;
        _worksheet.Cell(currentRow, currentColumn).Style.Fill.BackgroundColor = HeaderColor;
        currentColumn++;
    }

    private void FormatStartOfTheMonthCell()
    {
        _worksheet.Cell(currentRow, 1).Style.Border.TopBorder = BorderThickness;
        _worksheet.Cell(currentRow, 2).Style.Border.TopBorder = BorderThickness;
        _worksheet.Cell(currentRow, 3).Style.Border.TopBorder = BorderThickness;
        _worksheet.Cell(currentRow, 4).Style.Border.TopBorder = BorderThickness;
        _worksheet.Cell(currentRow, 5).Style.Border.TopBorder = BorderThickness;
        _worksheet.Cell(currentRow, 6).Style.Border.TopBorder = BorderThickness;
    }

    private void SetEntry(Output entry)
    {
        _worksheet.Cell(currentRow, 2).Style.Border.LeftBorder = BorderThickness;
        _worksheet.Cell(currentRow, 2).Value = entry.Name;
        _worksheet.Cell(currentRow, 2).Style.Border.RightBorder = BorderThickness;
        _worksheet.Cell(currentRow, 3).Value = entry.Expend;
        _worksheet.Cell(currentRow, 3).Style.Border.RightBorder = BorderThickness;
        _worksheet.Cell(currentRow, 4).Value = entry.IPExpend;
        _worksheet.Cell(currentRow, 4).Style.Border.RightBorder = BorderThickness;
        _worksheet.Cell(currentRow, 5).Value = entry.Income;
        _worksheet.Cell(currentRow, 5).Style.Border.RightBorder = BorderThickness;
        _worksheet.Cell(currentRow, 6).Value = entry.IPIncome;
        _worksheet.Cell(currentRow, 6).Style.Border.RightBorder = BorderThickness;
        currentRow++;
    }
}