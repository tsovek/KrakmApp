using System.Security.Claims;

using KrakmApp.Core;
using KrakmApp.Core.Mappings;
using KrakmApp.Core.Repositories;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Core.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace KrakmApp
{
    public class Startup
    {
        private static string _applicationPath = string.Empty;
        public Startup(IHostingEnvironment env)
        {
            ApplicationEnvironment appEnv = new ApplicationEnvironment();
            _applicationPath = appEnv.ApplicationBasePath;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
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
            services.AddDbContext<KrakmAppContext>(options =>
                        options.UseSqlServer(Configuration["Data:KrakmAppConnection:ConnectionString"]));

            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IPartnersRepository, PartnersRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IEntertainmentRepository, EntertainmentRepository>();
            services.AddScoped<IMonumentRepository, MonumentsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ILoggingRepository, LoggingRepository>();
            services.AddScoped<ILocalizationRepository, LocalizationRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IBannersRepository, BannersRepository>();
            services.AddScoped<IRouteDetailsRepository, RouteDetailsRepository>();

            // Services
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IRandomGeneratorService, RandomGeneratorService>();
            services.AddScoped<IRouteDetailsFactory, RouteDetailsFactory>();
            services.AddScoped<IObjectsService, ObjectsService>();
            services.AddScoped<IAllRoutesFactory, AllRoutesFactory>();

            services.AddAuthentication();

            // Polices
            services.AddAuthorization(options =>
            {
                // inline policies
                options.AddPolicy("AdminOnly", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "Admin");
                });
                options.AddPolicy("All", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, 
                        "Admin", "Owner", "Employee", "Partner");
                });
                options.AddPolicy("OwnerOnly", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "Owner", "Admin");
                });
            });

            // Add MVC services to the services container.
            services.AddMvc();
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app)
        {
            // Add static files to the request pipeline.
            app.UseStaticFiles();
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            app.UseDeveloperExceptionPage();

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
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseUrls("http://0.0.0.0:5000")
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
