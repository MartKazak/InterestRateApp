using System;
using System.Text.Json.Serialization;
using InterestRateApp.DataAccess;
using InterestRateApp.Infrastructure.Services;
using InterestRateApp.Services.Processors;
using InterestRateApp.Services.Repositories;
using InterestRateApp.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace InterestRateApp.API
{
    public class Startup
    {
        private const string ApiName = "InterestRateApp API";

        public Startup(IConfiguration configuration) => 
            Configuration = configuration;

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddScoped<IDatabaseContext>(provider => provider.GetService<DatabaseContext>());
            
            ConfigureService(services);
            ConfigureRepositories(services);
            ConfigureDatabase(services);
            ConfigureHttpClients(services);

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = ApiName, Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            db.Database.EnsureCreated();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", $"{ApiName} V1");
            });

            app.Use(async (httpContext, next) =>
            {
                if (string.IsNullOrEmpty(httpContext.Request.Path) 
                    || httpContext.Request.Path == "/" 
                    || httpContext.Request.Path == "/api")
                {
                    httpContext.Response.Redirect(httpContext.Request.PathBase + "/swagger");
                    return;
                }

                await next();
            });

            app.UseHttpsRedirection();
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("InterestRateAppDatabase");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string was not found");
            }

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        private void ConfigureHttpClients(IServiceCollection services)
        {
            services.AddHttpClient<IBaseRateService, BaseRateService>(client =>
            {
                var baseUrl = new Uri(Configuration.GetValue<string>("LbWebserviceUrl:Base"));
                var baseRateUrl = Configuration.GetValue<string>("LbWebserviceUrl:BaseRateUrl");
                client.BaseAddress = new Uri(baseUrl, baseRateUrl);
            });
        }

        private static void ConfigureService(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAgreementService, AgreementService>();
            services.AddScoped<IAgreementProcessor, AgreementProcessor>();
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAgreementRepository, AgreementRepository>();
        }
    }
}
