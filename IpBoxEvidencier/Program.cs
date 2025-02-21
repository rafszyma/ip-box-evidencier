// See https://aka.ms/new-console-template for more information


using IpBoxEvidencier;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddTransient<CreateSheet>();
services.AddTransient<KPiRReader>();
services.AddTransient<Weighter>();
var provider = services.BuildServiceProvider();


var reader = provider.GetRequiredService<KPiRReader>();
var months = reader.Read();

var sheetCreator = provider.GetRequiredService<CreateSheet>();


sheetCreator.CreateExcel(months);

Console.WriteLine("Im done!");