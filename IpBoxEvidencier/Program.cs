// See https://aka.ms/new-console-template for more information


using IpBoxEvidencier;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddTransient<CreateSheet>();
var provider = services.BuildServiceProvider();

var sheetCreator = provider.GetRequiredService<CreateSheet>();


sheetCreator.CreateExcel();

Console.WriteLine("Hello, World!");