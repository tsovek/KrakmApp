using System.Security.Claims;

using KrakmApp.Core;
using KrakmApp.Core.Mappings;
using KrakmApp.Core.Repositories;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Core.Services;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace KrakmApp
{
    public class Startup
    {
        private static string _applicationPath = string.Empty;
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            _applicationPath = appEnv.ApplicationBasePath;

            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                    .AddSqlServer()
                    .AddDbContext<KrakmApp.Core.KrakmAppContext>(options =>
                        options.UseSqlServer(Configuration["Data:KrakmAppConnection:ConnectionString"]));

            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IPartnersRepository, PartnersRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IEntertainmentRepository, EntertainmentRepository>();
            services.AddScoped<IMonumentRepository, MonumentsRepository>();
            services.AddScoped<IMarkerRepository, MarkerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ILoggingRepository, LoggingRepository>();
            services.AddScoped<ILocalizationRepository, LocalizationRepository>();

            // Services
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IEncryptionService, EncryptionService>();

            services.AddAuthentication();

            // Polices
            services.AddAuthorization(options =>
            {
                // inline policies
                options.AddPolicy("AdminOnly", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "Admin");
                });
            });

            // Add MVC services to the services container.
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            // Add the platform handler to the request pipeline.
            app.UseIISPlatformHandler();

            // Add static files to the request pipeline.
            app.UseStaticFiles();

            app.UseCookieAuthentication(options =>
            {
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = true;
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });

            AutoMapperConfiguration.Configure();

            DbInitializer.Initialize(app.ApplicationServices, _applicationPath);
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
