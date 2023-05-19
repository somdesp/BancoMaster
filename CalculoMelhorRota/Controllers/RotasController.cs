using CalculoMelhorRota.Application.Interfaces.AppServices;
using CalculoMelhorRota.Application.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CalculoMelhorRota.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RotasController : ControllerBaseGlobal
    {
        private readonly IRotasAppService _rotasAppService;

        public RotasController(IRotasAppService rotasAppService)
        {
            _rotasAppService = rotasAppService;
        }

        [HttpPut]
        [Route("Insert")]
        public IActionResult Insert(IEnumerable<RotasViewModel> rotas, CancellationToken cancellationToken)
        {
            var result = _rotasAppService.Insert(rotas, cancellationToken);
            return Result(result, _rotasAppService.StatusCode, _rotasAppService.Message, _rotasAppService.Success, _rotasAppService.Errors);
        }

        [HttpGet]
        [Route("MelhorRota/{rotas}")]
        public IActionResult MelhorRota(string rotas, CancellationToken cancellationToken)
        {
            var result = _rotasAppService.MelhorRota(rotas, cancellationToken);
            return Result(result, _rotasAppService.StatusCode, _rotasAppService.Message, _rotasAppService.Success, _rotasAppService.Errors);
        }
    }
    public abstract class ControllerBaseGlobal : ControllerBase
    {
        private struct APIResult<TResult>
        {
            public string Message { get; set; }
            public bool Success { get; set; }
            public TResult Data { get; set; }
            public IEnumerable<string> Errors { get; set; }
        }

        public virtual ObjectResult Result<TResult>(TResult result, HttpStatusCode httpStatusCode, string message, bool success, IEnumerable<string> errors)
        {
            return StatusCode((int)httpStatusCode, new APIResult<TResult>()
            {
                Data = result,
                Message = message,
                Success = success,
                Errors = errors
            });
        }
    }
}
