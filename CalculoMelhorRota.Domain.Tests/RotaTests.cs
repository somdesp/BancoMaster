using CalculoMelhorRota.CrossCutting.Util.Configs;
using CalculoMelhorRota.Domain.Entity;
using CalculoMelhorRota.Domain.Interfaces;
using CalculoMelhorRota.Domain.Service;
using CalculoMelhorRota.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CalculoMelhorRota.Domain.Tests
{
    public class RotaTests
    {
        [Fact]
        public void Rota_Insert()
        {
            // arrange
            var someOptions = Options.Create(new AppSettingsUtils() { Path = "D:\\Satelos\\Projetos\\Master\\BancoMaster" });
            var mockNotifier = new Mock<INotifier>();
            var mockRotaRepository = new RotasRepository(someOptions);

            // act 
            var rotaService = new RotasService(mockNotifier.Object, mockRotaRepository);
            var rotas = new List<Rotas>();

            rotas.Add(new Rotas { Origem = "GRU", Destino = "BRC" });
            var result = rotaService.Insert(rotas);

            // assert
            Assert.Equal(rotas.Count(), result.Count());

        }

        [Theory]
        [InlineData("GRU", "BRC", "Melhor Rota: GRU - BRC ao custo de R$:10")]
        [InlineData("GRU", "CDG", "Melhor Rota: GRU - BRC ao custo de R$:40")]
        [InlineData("SCL", "GRU", "Melhor Rota: GRU - BRC ao custo de R$:45")]
        [InlineData("BRC", "SCL", "Melhor Rota: GRU - BRC ao custo de R$:5")]

        public void Melhor_Rota(string origem, string destino, string resultado)
        {
            // arrange
            var appSettingsOptions = Options.Create(new AppSettingsUtils() { Path = "D:\\Satelos\\Projetos\\Master\\BancoMaster" });
            var mockNotifier = new Mock<INotifier>();
            var mockRotaRepository = new RotasRepository(appSettingsOptions);

            // act 
            var rotaService = new RotasService(mockNotifier.Object, mockRotaRepository);
            var resultadoExecucao = rotaService.MelhorRota($"{origem}-{destino}");

            // assert
            Assert.Equal(resultado, resultadoExecucao);

        }

        //[Fact]
        public void Get_Insert()
        {

            // arrange
            var someOptions = Options.Create(new AppSettingsUtils() { Path = "D:\\Satelos\\Projetos\\Master\\BancoMaster" });
            var mockNotifier = new Mock<INotifier>();
            var mockRotaRepository = new RotasRepository(someOptions);


            // act 
            var rotaService = new RotasService(mockNotifier.Object, mockRotaRepository);
            var result = rotaService.GetAll();

            // assert
            result.DefaultIfEmpty();

        }
    }
}
