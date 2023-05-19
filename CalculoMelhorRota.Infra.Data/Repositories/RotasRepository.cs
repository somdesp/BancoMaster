﻿using CalculoMelhorRota.CrossCutting.Util.Configs;
using CalculoMelhorRota.Domain.Entity;
using CalculoMelhorRota.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace CalculoMelhorRota.Infra.Data.Repositories
{
    public class RotasRepository : IRotasRepository
    {
        private readonly AppSettingsUtils _settingsUtils;

        public RotasRepository(IOptions<AppSettingsUtils> settingsUtils)
        {
            _settingsUtils = settingsUtils.Value;
        }
        public List<Rotas> GetRotas()
        {
            var rotas = new List<Rotas>();
            string pathCSV = @$"{_settingsUtils.Path}\rotas.csv";

            if (!File.Exists(pathCSV))
                return rotas;

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
            return rotas;
        }

        public void Insert(List<Rotas> rotasInseridas)
        {
            string pathCSV = @$"{_settingsUtils.Path}\rotas.csv";

            if (!File.Exists(pathCSV))
                File.AppendAllText(pathCSV, "");

            using (StreamWriter writer = new StreamWriter(pathCSV, false))
                {
                    foreach (var line in rotasInseridas)
                        writer.WriteLine($"{line.Origem},{line.Destino},{line.Valor}");
                }
            
        }
    }
}