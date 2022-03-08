
using Basket.Api.Configuration;
using Basket.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Basket.Api
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
           
            services
                .AddSwaggerModule()
                .AddWebModule()
                .AddConsul(Configuration);
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IConsulHttpClient, ConsulHttpClient>();
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IHostApplicationLifetime hostApplicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseApplicationWeb(env);
            app.UseConsul(hostApplicationLifetime,Configuration);
            app.UseApplicationSwagger();
            //app.UseAuthorization();

           
        }
    }
}
