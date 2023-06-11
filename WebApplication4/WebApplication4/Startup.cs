using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
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
                string method = contex.Request.Method;

                String[] array = path.Split("/");
                String err = "";
                String index = "";
                try
                {
                    err = array[2];       
                    index = array[3];    
                    
                }
                catch
                {
                }
                
                if (path == "/api/allData")
                {

                    string content = Responses.All(jsonpath);
                    contex.Response.Headers["Content-Type"] = "text/html";
                    await contex.Response.WriteAsync(content);
                }
                else if (path == "/api/scan") 
                {
                    string content = Responses.Scan(jsonpath);
                    contex.Response.Headers["Content-Type"] = "text/html";
                    await contex.Response.WriteAsync(content);
                }
                if (path == "/api/errors")
                {
                    IEnumerable<Files> content = Responses.Errors(jsonpath);
                    contex.Response.Headers["Content-Type"] = "text/html";
                    string HTMLcontent = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset = utf-8\"/>";
                    foreach(var item in content)
                    {
                        HTMLcontent += item.filename + " - ";
                        foreach(var error in item.errors)
                        {
                            HTMLcontent += error.error + "<br>";
                        }
                    }
                    await contex.Response.WriteAsync(HTMLcontent);
                }
                else if(path== "/api/errors/count")
                {
                    string content = Responses.ErrorsCount(jsonpath);
                    contex.Response.Headers["Content-Type"] = "text/html";
                    await contex.Response.WriteAsync(content);
                }else if(path == "/api/filenames")
                {
                    if (contex.Request.Query.ContainsKey("correct"))
                    {
                        string quer = contex.Request.Query["correct"];
                        if (quer=="false")
                        {
                            string content = Responses.Filename(jsonpath,false);
                            contex.Response.Headers["Content-Type"] = "text/html";
                            await contex.Response.WriteAsync(content);
                        }else if (quer == "true")
                        {
                            string content = Responses.Filename(jsonpath, true);
                            contex.Response.Headers["Content-Type"] = "text/html";
                            await contex.Response.WriteAsync(content);
                        }
                    }    
                }else if (err == "errors" && index != null)
                {
                    contex.Response.Headers["Content-Type"] = "text/html";
                    try
                    {
                        int int_index = Convert.ToInt32(index);
                        IEnumerable<Files> content = Responses.Errors_Index(jsonpath, int_index);
                        string HTMLcontent = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset = utf-8\"/>";
                        foreach (var item in content)
                        {
                            HTMLcontent += item.filename + " - ";
                            foreach (var error in item.errors)
                            {
                                HTMLcontent += error.error + "<br>";
                            }
                        }
                        await contex.Response.WriteAsync(HTMLcontent);
                    }
                    catch { await contex.Response.WriteAsync("index" + index + "not found"); }
                    
                }else if(path == "/api/query/check")
                {
                    IEnumerable<QuerryCheck> content = Responses.QuerryCheck(jsonpath);
                    contex.Response.Headers["Content-Type"] = "text/html";
                    string HTMLcontent = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset = utf-8\"/>";
                    foreach (var item in content)
                    {
                        HTMLcontent += "TotalFiles - " + item.TotalFiles + " <br> ";
                        HTMLcontent += "TotalCorrectFiles - " + item.TotalCorrectFiles + " <br> ";
                        HTMLcontent += "TotalIncorrectFiles - " + item.TotalIncorrectFiles + " <br> ";
                        foreach (var fname in item.IncorrectFilenames)
                        {
                           HTMLcontent += "IncorrectFilename - " + fname + "<br>";
                        }
                    }
                    await contex.Response.WriteAsync(HTMLcontent);
                }
                else if(path == "/api/service/serviceInfo")
                {
                    IEnumerable<ServiceInfo> content = Responses.GetServiceInfo();
                    contex.Response.Headers["Content-Type"] = "text/html";
                    string HTMLcontent = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset = utf-8\"/>";
                    foreach (var item in content)
                    {
                        HTMLcontent += "AppName - " + item.AppName.ToString() + "<br>";
                        HTMLcontent += "Version - " + item.Version.ToString() + "<br>";
                        HTMLcontent += "DateUtc - " + item.DateUtc;
                    }
                    await contex.Response.WriteAsync(HTMLcontent);
                }else if(path == "/api/newErrors" && method == "POST")
                {
                    contex.Response.Headers["Content-Type"] = "text/html";
                    string HTMLcontent = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset = utf-8\"/>";
                    StreamReader streamReader = new StreamReader(contex.Request.Body);
                    string data = await streamReader.ReadToEndAsync();

                    if (!Responses.AddNewErrors(data))
                    { await contex.Response.WriteAsync("Данные не правильные! Ошибка парсинга!");  }
                    
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
