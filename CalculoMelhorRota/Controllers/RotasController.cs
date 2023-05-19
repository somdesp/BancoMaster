using CalculoMelhorRota.API.Config;
using CalculoMelhorRota.Application.Interfaces.AppServices;
using CalculoMelhorRota.Application.ViewsModels;
using CalculoMelhorRota.CrossCutting.Util.Configs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;

namespace CalculoMelhorRota.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RotasController : MainController
    {
        private readonly IRotasAppService _rotasAppService;

        public RotasController(INotifier notifier, IRotasAppService rotasAppService) : base(notifier)
        {
            _rotasAppService = rotasAppService;
        }

        [HttpPut]
        [Route("Insert")]
        public IActionResult Insert(IEnumerable<RotasViewModel> rotas, CancellationToken cancellationToken)
        {
            var result = _rotasAppService.Insert(rotas, cancellationToken);
            return CustomResponse(result);
        }

        [HttpGet]
        [Route("MelhorRota/{rotas}")]
        public IActionResult MelhorRota(string rotas, CancellationToken cancellationToken)
        {
            var result = _rotasAppService.MelhorRota(rotas, cancellationToken);
            return CustomResponse(result);
        }
    }

}
