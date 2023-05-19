using System.Collections.Generic;
using System.Net;

namespace CalculoMelhorRota.Application.Interfaces.AppServices
{
    public interface IGlobalAppService
    {
        HttpStatusCode StatusCode { get; set; }
        string Message { get; set; }
        bool Success { get; set; }
        List<string> Errors { get; set; }
    }
}
