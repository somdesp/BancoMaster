using CalculoMelhorRota.CrossCutting.Util.Configs;
using CalculoMelhorRota.Domain.Entity;
using CalculoMelhorRota.Domain.Interfaces;
using EConstrumarket.Construmanager.Core.CrossCutting.IoC.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CalculoMelhorRotaConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: true);

            var Configuration = builder.Build();


            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCustomService();    

            serviceCollection.Configure<AppSettingsUtils>(Configuration.GetSection(nameof(AppSettingsUtils)));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var eventService = serviceProvider.GetService<IRotasService>();

            var rotas = new List<Rotas>();


            string pathCSV = "";
            if (!args.Any())
            {
                pathCSV = $@"{Directory.GetCurrentDirectory()}\rotas.csv";
            }
            else { pathCSV = args[0]; }

            using (TextFieldParser csvParser = new TextFieldParser(pathCSV))
            {
                csvParser.TextFieldType = FieldType.Delimited;
                csvParser.SetDelimiters(",");

                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    rotas.Add(new Rotas
                    {
                        Origem = fields[0],
                        Destino = fields[1],
                        Valor = Convert.ToInt32(fields[2])
                    });
                }
            }

            eventService.Insert(rotas);
            Console.WriteLine("Digite a rota:");

            var rotakey = Console.ReadLine();
            var resultadoFinal = eventService.MelhorRota(rotakey);
            Console.WriteLine(resultadoFinal);
            Console.ReadLine();
        }

    }
}
