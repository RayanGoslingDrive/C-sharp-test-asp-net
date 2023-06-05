using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication4.Data.Interfaces;
using WebApplication4.Data.mocks;
using WebApplication4.Data.Models;

namespace WebApplication4
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //
            services.AddMvc();
            services.AddMvc(option => option.EnableEndpointRouting = false );
            services.AddTransient<_Interface, mockinfo>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseDefaultFiles();



            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
                /*routes.MapRoute(
                    name: "scan",
                    template: "{controller=Home}/{action=Scan}");*/
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                   // await context.Response.WriteAsync($"scanTime: {scanInfo.scanTime}");
                });
            });
        }
    }
}
