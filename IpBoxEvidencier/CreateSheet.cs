using ClosedXML.Excel;

namespace IpBoxEvidencier;

public class CreateSheet
{
    private const string Output = "E:\\CodeHome\\Payload\\excel.xlsx";
    
    private IXLWorksheet _worksheet;

    private IXLWorkbook _workbook;

    public CreateSheet()
    {
        if (File.Exists(Output))
        {
            File.Delete(Output);
        }
        
        _workbook = new XLWorkbook();
        _worksheet = _workbook.Worksheets.Add(DateTime.UtcNow.Year);
    }

    public void CreateExcel()
    {
        SetHeaders();

        
        _workbook.SaveAs(Output);
    }

    private void SetHeaders()
    {
        _worksheet.Cell(1, 1).Value = "Miesiąc";
        _worksheet.Cell(1, 2).Value = "Nazwa";
        _worksheet.Cell(1, 3).Value = "Wydatki";
        _worksheet.Cell(1, 4).Value = "Wydatki IP";
        _worksheet.Cell(1, 5).Value = "Nazwa";
        _worksheet.Cell(1, 6).Value = "Przychód";
        _worksheet.Cell(1, 7).Value = "Przychód IP";
        _worksheet.Cell(1, 8).Value = "Uwagi";
    }
}