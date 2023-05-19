﻿using CalculoMelhorRota.Application.ViewsModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CalculoMelhorRota.Application.Interfaces.AppServices
{
    public interface IRotasAppService : IGlobalAppService
    {
        IEnumerable<RotasViewModel> Insert(IEnumerable<RotasViewModel> rotas, CancellationToken cancellationToken);
        string MelhorRota(string rotas, CancellationToken cancellationToken);
    }
}