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

namespace CalculoMelhorRotaConsole.Service
{
    public class AppService
    {
        public void ExecutaCalculoRota(string pathCSV)
        {
            #region Injeção de dependencia
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: true);

            var Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCustomService();

            serviceCollection.Configure<AppSettingsUtils>(Configuration.GetSection(nameof(AppSettingsUtils)));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var rotasService = serviceProvider.GetService<IRotasService>();
            #endregion

            var rotas = new List<Rotas>();

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
            rotasService.Insert(rotas);

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Digite a rota:");
                var rotakey = Console.ReadLine();
                var resultadoFinal = rotasService.MelhorRota(rotakey);
                Console.WriteLine(resultadoFinal);
            }
        }
    }
}
