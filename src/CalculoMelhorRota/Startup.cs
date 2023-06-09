using CalculoMelhorRota.API.Config.Api;
using CalculoMelhorRota.CrossCutting.IOC.AutoMapping;
using CalculoMelhorRota.CrossCutting.IOC.DependencyInjection;
using CalculoMelhorRota.CrossCutting.Util.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CalculoMelhorRota.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCustomService();
            services.WebApiConfig();
            services.AddCustomAutoMapping(Configuration);
            services.Configure<AppSettingsUtils>(Configuration.GetSection(nameof(AppSettingsUtils)));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Configure(env, provider);

        }
    }
}
