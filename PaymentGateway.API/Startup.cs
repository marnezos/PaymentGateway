using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PaymentGateway.Application.DTOs.Payments;
using PaymentGateway.Application.Services.Payments;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Rebus.Transport.InMem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rebus.Persistence.InMem;
using Rebus.Config;
using PaymentGateway.Application.Interfaces.Storage.Write;
using PaymentGateway.Application.Interfaces.Storage.Read;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.IdentityModel.Logging;
using PaymentGateway.Application.Services.Bank;
using Rebus.Retry.Simple;

namespace PaymentGateway.API
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentGateway.API", Version = "v1" });
            });
            services.AddLogging();

            services.AddRebus(configure => configure
                .Logging(l => l.None())
                .Options(o=>o.EnableSynchronousRequestReply())
                .Options(o=>o.SimpleRetryStrategy(maxDeliveryAttempts:1))
                .Transport(t => t.UseInMemoryTransport(new InMemNetwork(false), Configuration.GetValue<string>("SBQueueName")))
                .Routing(r => r.TypeBased().MapAssemblyOf<PaymentProcessRequestDto>(Configuration.GetValue<string>("SBQueueName")))
            );

            services.AutoRegisterHandlersFromAssemblyOf<AcquiringBankMessageHandler>();
            services.AutoRegisterHandlersFromAssemblyOf<ProcessPaymentService>();


            services.AddScoped<IPeristentWriteOnlyStorage>(x => new Persistence.InMemory.PersistentWriteOnlyStorage(new Persistence.InMemory.InMemoryPersistenceOptions()
            {
                InMemoryDbName = "memdb"
            }));
            services.AddScoped<IPersistentReadOnlyStorage>(x => new Persistence.InMemory.PersistentReadOnlyStorage(new Persistence.InMemory.InMemoryPersistenceOptions()
            {
                InMemoryDbName = "memdb"
            }));

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:4999"; //is4 server base url (ToDo: config)
                    options.RequireHttpsMetadata = false; //Development, allow http 
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                    options.IncludeErrorDetails = true;

                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("PaymentGatewayScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "paymentgateway.api"); //whole api scope
                });
                options.AddPolicy("MerchantsOnly", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "paymentgateway.api");
                    policy.RequireClaim(ClaimTypes.Role, "paymentgateway.merchant"); //merchant role
                });
            });

            IdentityModelEventSource.ShowPII = true;
        }

    

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentGateway.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                .RequireAuthorization("PaymentGatewayScope"); //Our API is available only to claiming scope: paymentgateway.api
            });

            app.ApplicationServices.UseRebus();

        }
    }
}
