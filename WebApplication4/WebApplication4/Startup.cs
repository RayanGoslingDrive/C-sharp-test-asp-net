using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication4.Data;

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

            string jsonpath = @"C:\Users\vsh21\source\repos\WebApplication4\WebApplication4\data2.json";

            app.Run(async (HttpContext contex) =>
            {
                string path = contex.Request.Path;

                if (path == "/api/allData")
                {

                    string content = Responses.All(jsonpath, contex);
                    await contex.Response.WriteAsync(content);
                }
                else if (path == "/api/scan") 
                {
                    string content = Responses.Scan(jsonpath, contex);
                    contex.Response.Headers["Content-Type"] = "text/html";
                    await contex.Response.WriteAsync(content);
                }else if (path == "/api/errors")
                {
                    string content = Responses.Errors(jsonpath, contex);
                    contex.Response.Headers["Content-Type"] = "text/html";
                    await contex.Response.WriteAsync(content);
                }else if(path== "/api/errors/count")
                {
                    string content = Responses.ErrorsCount(jsonpath, contex);
                    contex.Response.Headers["Content-Type"] = "text/html";
                    await contex.Response.WriteAsync(content);
                }else if(path == "/api/filenames")
                {
                    if (contex.Request.Query.ContainsKey("correct"))
                    {
                        string quer = contex.Request.Query["correct"];
                        if (quer=="false")
                        {
                            string content = Responses.Filename(jsonpath, contex,false);
                            contex.Response.Headers["Content-Type"] = "text/html";
                            await contex.Response.WriteAsync(content);
                        }else if (quer == "true")
                        {
                            string content = Responses.Filename(jsonpath, contex, true);
                            contex.Response.Headers["Content-Type"] = "text/html";
                            await contex.Response.WriteAsync(content);
                        }
                    }    
                }
            });

         


            /*app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
                *//*routes.MapRoute(
                    name: "scan",
                    template: "{controller=Home}/{action=Scan}");*//*
            });*/


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
