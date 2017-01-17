using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Model;
using Minor.WSA.Commons;
using Minor.WSA.EventBus.Publisher;
using Jomaya.Klantenservice.Infrastructure.Repositories;
using Jomaya.Klantenservice.Infrastructure.DAL;
using Jomaya.Klantenservice.Entities;
using Jomaya.Klantenservice.Services;
using Minor.WSA.EventBus.Config;
using MySQL.Data.EntityFrameworkCore.Extensions;
using Jomaya.Klantenservice.Services.Interfaces;

namespace Jomaya.Klantenservice.Facade
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddSwaggerGen();

            var sqlConnectionString = Configuration.GetConnectionString("DataAccessMySqlProvider")
                ?? "server=localhost;port=4001;userid=jomaya;password=jomaya;database=jomaya-klantservice;";
            
            services.AddDbContext<KlantContext>(options => options.UseMySQL(sqlConnectionString));
            
            //b => b.MigrationsAssembly("AspNet5MultipleProject")

            services.AddScoped<IRepository<Klant, long>, KlantRepository>();
            services.AddScoped<IEventPublisher, EventPublisher>(config =>
            {
                var _config = new EventBusConfig()
                {
                    Host = Configuration.GetConnectionString("RabbitMQ") ?? "localhost",
                    Port = 5672,
                };
                
                return new EventPublisher(_config);
            });
            services.AddScoped<KlantService>();


            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "A Monument service",
                    Description = "Restauration of monuments",
                    TermsOfService = "None"
                });
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
