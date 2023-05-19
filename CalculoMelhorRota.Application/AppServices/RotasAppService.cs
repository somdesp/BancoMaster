using AutoMapper;
using CalculoMelhorRota.Application.Interfaces.AppServices;
using CalculoMelhorRota.Application.ViewsModels;
using CalculoMelhorRota.Domain.Entity;
using CalculoMelhorRota.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CalculoMelhorRota.Application.AppServices
{
    public class RotasAppService : GlobalAppService, IRotasAppService
    {
        private readonly IRotasService _rotaService;
        private readonly IMapper _mapper;

        public RotasAppService(IRotasService rotasService, IMapper mapper)
        {
            _rotaService = rotasService;
            _mapper = mapper;
        }

        public IEnumerable<RotasViewModel> Insert(IEnumerable<RotasViewModel> rotas, CancellationToken cancellationToken)
        {
            try
            {
                var rotasResult = _rotaService.Insert(_mapper.Map<IEnumerable<Rotas>>(rotas));
                var result = _mapper.Map<IEnumerable<RotasViewModel>>(rotasResult);
                return result;
            }
            catch (Exception ex)
            {
                Success = false;
                StatusCode = HttpStatusCode.InternalServerError;
                Message = ex.ToString();
                return null;
            }
        }

        public string MelhorRota(string rotas, CancellationToken cancellationToken)
        {
            try
            {
                var rotasResult = _rotaService.MelhorRota(rotas);
                return rotasResult;
            }
            catch (Exception ex)
            {
                Success = false;
                StatusCode = HttpStatusCode.InternalServerError;
                Message = ex.ToString();
                return null;
            }
        }
    }
}
