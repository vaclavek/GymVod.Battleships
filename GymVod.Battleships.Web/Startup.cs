using GymVod.Battleships.DataLayer;
using GymVod.Battleships.DataLayer.Repositories;
using GymVod.Battleships.DataLayer.UnitOfWorks;
using GymVod.Battleships.Services;
using GymVod.Battleships.Services.GameServer;
using GymVod.Battleships.Services.Players;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Sotsera.Blazor.Toaster.Core.Models;

namespace GymVod.Battleships.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            // infrastructure
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // uow, dbcontext
            services.AddScoped<MyContext, MyContext>();
            services.AddScoped<DbContext, MyContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // repositories
            services.AddScoped<IPlayerRepository, PlayerRepository>();

            // services
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IPluginTester, PluginTester>();
            services.AddScoped<IGameEngine, GameEngine>();

            // Add the library to the DI system
            services.AddToaster(config =>
            {
                //example customizations
                config.PositionClass = Defaults.Classes.Position.TopRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
