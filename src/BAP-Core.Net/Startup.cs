using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAP_Core.Net.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BAP_Core.Net
{
    public class Startup
    {
        private readonly IConfigurationRoot _config;
        private readonly IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _config = builder.Build();
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //singleton of service from configuration create in startup constructor
            services.AddSingleton(_config);
            // Add framework services.
            if (_env.IsDevelopment() || _env.IsEnvironment("test"))
                services.AddScoped<IMailService, DebugMailService>(); // add the sendMail service
            else
            //Implement real email service
            {
            }
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=App}/{action=Index}/{id?}");
            });
        }
    }
}
