using CalculoMelhorRota.Application.AppServices;
using CalculoMelhorRota.Application.Interfaces.AppServices;
using CalculoMelhorRota.Domain.Interfaces;
using CalculoMelhorRota.Domain.Service;
using CalculoMelhorRota.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EConstrumarket.Construmanager.Core.CrossCutting.IoC.DependencyInjection
{

    public static class CustomService
    {

        public static void AddCustomService(this IServiceCollection services)
        {
            #region Injeções Serviços
            services.AddScoped<IRotasAppService, RotasAppService>()
                    .AddScoped<IRotasService, RotasService>()
                    .AddScoped<IGlobalAppService, GlobalAppService>();
            #endregion

            #region Injeções Repositórios

            services.AddScoped<IRotasRepository, RotasRepository>();

            #endregion

        }
    }
}
