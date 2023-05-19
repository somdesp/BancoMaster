using CalculoMelhorRota.Application.Interfaces.AppServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Net;

namespace CalculoMelhorRota.Application.AppServices
{
    public class GlobalAppService : IGlobalAppService
    {
        protected int ErrorHttp;

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotImplemented;
        public string Message { get; set; }
        public bool Success { get; set; } = false;
        public List<string> Errors { get; set; } = new List<string>();
        public ModelStateDictionary ModelState { get; } = new ModelStateDictionary();


    }

}
