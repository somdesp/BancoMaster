using CalculoMelhorRota.Domain.Entity;
using CalculoMelhorRota.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CalculoMelhorRota.Domain.Service
{
    public class RotasService : IRotasService
    {
        private readonly IRotasRepository _repository;
        public RotasService(IRotasRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<Rotas> Insert(IEnumerable<Rotas> rotas)
        {
            List<Rotas> rotasInseridas = _repository.GetRotas();
            rotasInseridas.AddRange(rotas);

            rotasInseridas = rotasInseridas.GroupBy(x => new { x.Origem, x.Destino }).Select(o => o.First()).ToList();
            _repository.Insert(rotasInseridas);

            return rotas;
        }

        public string MelhorRota(string srcRotas)
        {
            List<Rotas> rotas = _repository.GetRotas();

            rotas = rotas.OrderByDescending(x => x.Valor).ToList();

            var origem = srcRotas.Split('-')[0];
            var destino = srcRotas.Split('-')[1];

            var origens = rotas.Where(x => x.Origem.ToLower() == origem.ToLower()).ToList();

            List<Resultado> resultados = new List<Resultado>();

            for (var k = 0; k < origens.Count(); ++k)
            {
                string destinoAux = origens[k].Destino;
                string destinoFinal = origens[k].Origem;
                int valorFinal = origens[k].Valor;

                for (int j = 0; j < rotas.Count(); j++)
                {
                    if (destino.ToLower() == destinoAux.ToLower())
                    {
                        break;
                    }

                    for (int i = 0; i < rotas.Count(); i++)
                    {
                        if (rotas[i].Origem.ToLower() == destinoAux.ToLower())
                        {
                            valorFinal = valorFinal + rotas[i].Valor;
                            destinoFinal = destinoFinal + "-" + rotas[i].Origem;
                            destinoAux = rotas[i].Destino;
                        }
                    }
                }

                if (destino.ToLower() != destinoAux.ToLower())
                    continue;

                destinoFinal = destinoFinal + "-" + destinoAux;
                resultados.Add(new Resultado
                {
                    Rota = destinoFinal,
                    Valor = valorFinal
                });
            }

            var resultadoFinal = resultados.OrderBy(x => x.Valor).FirstOrDefault();
            string result = "";
            if (resultadoFinal == null)
                result = "Não foi possivel calcular uma rota.";
            else
                result = "Melhor Rota: " + resultadoFinal.Rota + " ao custo de R$:" + resultadoFinal.Valor;

            return result;
        }
    }
}
