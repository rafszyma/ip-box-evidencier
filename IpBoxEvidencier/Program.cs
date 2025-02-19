// See https://aka.ms/new-console-template for more information


using IpBoxEvidencier;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddTransient<CreateSheet>();
var provider = services.BuildServiceProvider();

var months = OutputMonth.Create();

// TEST DATA
months.First().Entries = new List<Output>
{
    new()
    {
        Name = "Orange",
        IPExpend = 40.64
    },
    new()
    {
        Name = "Ksiegowość",
        Expend = 258.5
    },
};
months.Skip(1).First().Entries = new List<Output>
{
    new()
    {
        Name = "Sprzedaż JIT",
        IPIncome = 30.3,
        Income = 10.1
    },
    new()
    {
        Name = "GitKraken",
        IPExpend = 261.47
    }
};

var sheetCreator = provider.GetRequiredService<CreateSheet>();


sheetCreator.CreateExcel(months);

Console.WriteLine("Hello, World!");