using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Model;
using Minor.WSA.Commons;
using Minor.WSA.EventBus.Config;
using Minor.WSA.EventBus.Publisher;
using Jomaya.AutoService.Entities;
using Jomaya.AutoService.Facade.Dispatchers;
using Jomaya.AutoService.Services.Interfaces;
using MySQL.Data.EntityFrameworkCore.Extensions;
using Jomaya.AutoService.Infrastructure.DAL;
using Jomaya.AutoService.Infrastructure.DAL.Repositories;

namespace Jomaya.AutoService.Facade
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
               ?? "server=localhost;port=5001;userid=jomaya;password=jomaya;database=jomaya-autoservice";

            services.AddDbContext<AutosBackendContext>(options =>
                    options.UseMySQL(sqlConnectionString));

            //, b => b.MigrationsAssembly("AspNet5MultipleProject")

            //services.AddDbContext<AutosBackendContext>(options => options.UseSqlServer(@"Server=db;Database=Jomaya;UserID=sa,Password=admin"));

            services.AddScoped<IRepository<Auto, long>, AutoRepository>();
            services.AddScoped<IRepository<Onderhoudsopdracht, long>, OnderhoudRepository>();

            services.AddSingleton<IEventPublisher, EventPublisher>(config =>
            {
                var _config = new EventBusConfig()
                {
                    Host = Configuration.GetConnectionString("RabbitMQ") ?? "localhost",
                    Port = 5672,
                    QueueName = "jomaya.autoservice",
                };


                return new EventPublisher(_config);
            });

            services.AddScoped<Services.AutoService, Services.AutoService>();
            services.AddScoped<AutoDispatcher, AutoDispatcher>();

            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Jomaya AutoService",
                    Description = "De service die de auto's beheert.",
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
