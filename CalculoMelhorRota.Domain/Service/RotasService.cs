using CalculoMelhorRota.CrossCutting.Util.Configs;
using CalculoMelhorRota.Domain.Entity;
using CalculoMelhorRota.Domain.Interfaces;
using CalculoMelhorRota.Domain.Validation;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace CalculoMelhorRota.Domain.Service
{
    public class RotasService : BaseService, IRotasService
    {
        private readonly IRotasRepository _repository;
        public RotasService(INotifier notifier, IRotasRepository repository) : base(notifier)
        {
            _repository = repository;
        }
        public IEnumerable<Rotas> Insert(IEnumerable<Rotas> rotas)
        {
            //Valida se os dados estao corretos
            var resulValidation = new RotasCollectionValidator();
            ValidationResult results = resulValidation.Validate(rotas);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Notification(failure.ErrorMessage);
                }
                return null;
            }

            List<Rotas> rotasInseridas = _repository.GetRotas();
            rotasInseridas.AddRange(rotas);
            //Remove duplicidades
            rotasInseridas = rotasInseridas.GroupBy(x => new { x.Origem, x.Destino }).Select(o => o.First()).ToList();
            _repository.Insert(rotasInseridas);

            return rotas;
        }

        public string MelhorRota(string srcRotas)
        {
            //Validação das entradas
            if (srcRotas.Split('-').Length != 2)
            {
                Notification("Rota deve seguir o padrão ex:(GRU-SCL)");
                return null;
            }

            var origem = srcRotas.Split('-')[0];
            var destino = srcRotas.Split('-')[1];

            //Retorna as rotas do CSV
            List<Rotas> rotas = _repository.GetRotas();
            //Filtra todas as origens possiveis
            var origens = rotas.Where(x => x.Origem.ToLower() == origem.ToLower()).ToList();
            //Valida se o Destino e origem existe
            if (!origens.Any())
            {
                Notification("Rota de origem Inválida.");
                return null;
            }
            if (!rotas.Where(x => x.Destino.ToLower() == destino.ToLower()).Any())
            {
                Notification("Rota de destino Inválida.");
                return null;
            }

            Resultado resultadoFinal = null;
            //Varre todas as origens possiveis
            for (var k = 0; k < origens.Count(); ++k)
            {
                var resultadoCaluclo = CalculoRota(rotas, origem, destino, origens[k].Destino, origens[k].Destino, origens[k].Valor);
                //Valida se o calculo teve algum sucesso
                if (resultadoCaluclo != null && (resultadoFinal == null || resultadoFinal.Valor > resultadoCaluclo.Valor))
                {
                    resultadoFinal = resultadoCaluclo;
                }
            }

            string result = "";
            if (resultadoFinal == null)
                result = "Não foi possivel calcular uma rota.";
            else
                result = "Melhor Rota: " + resultadoFinal.Rota + " ao custo de R$:" + resultadoFinal.Valor;

            return result;
        }

        Resultado CalculoRota(List<Rotas> rotas, string origem, string detinoFinal, string destinoCompleto, string destinoAtual, int valorRotaAtual)
        {
            //Pega as origens q possuem o destino do contexto
            var rotasDestinoAtual = rotas.Where(x => x.Origem == destinoAtual).OrderBy(x => x.Valor).FirstOrDefault();

            Resultado resultadoFinal;

            if (rotasDestinoAtual != null && destinoAtual != detinoFinal)
            {
                string rotaFinal = $@"{destinoCompleto} - {rotasDestinoAtual.Destino}";
                int valorRotaFinal = rotasDestinoAtual.Valor + valorRotaAtual;

                resultadoFinal = CalculoRota(rotas, origem, detinoFinal, rotaFinal, rotasDestinoAtual.Destino, valorRotaFinal);
            }
            else
            {
                string rotaFinal = $@"{origem} - {destinoCompleto}";
                resultadoFinal = new Resultado
                {
                    Rota = rotaFinal,
                    Valor = valorRotaAtual
                };
            }

            return resultadoFinal;
        }
    }
}
