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

            services.AddRebus(configure => configure
                .Logging(l => l.None())
                .Options(o=>o.EnableSynchronousRequestReply())
                .Transport(t => t.UseInMemoryTransport(new InMemNetwork(false), Configuration.GetValue<string>("SBQueueName")))
                .Routing(r => r.TypeBased().MapAssemblyOf<PaymentProcessRequestDto>(Configuration.GetValue<string>("SBQueueName")))
            );

            services.AutoRegisterHandlersFromAssemblyOf<ProcessPaymentService>();
            services.AddScoped<IPeristentWriteOnlyStorage>(x => new Persistence.InMemory.PersistentWriteOnlyStorage(new Persistence.InMemory.InMemoryPersistenceOptions()
            {
                InMemoryDbName = "memdb"
            }));
            services.AddScoped<IPersistentReadOnlyStorage>(x => new Persistence.InMemory.PersistentReadOnlyStorage(new Persistence.InMemory.InMemoryPersistenceOptions()
            {
                InMemoryDbName = "memdb"
            }));

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplicationServices.UseRebus();

        }
    }
}
