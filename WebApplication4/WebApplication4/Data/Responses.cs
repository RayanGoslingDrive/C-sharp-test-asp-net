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
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication4.Data.Models;

namespace WebApplication4.Data
{
    public class Responses
    {
        public static string All(string jsonpath, HttpContext contex)
        {

            string content = System.IO.File.ReadAllText(jsonpath);
            contex.Response.Headers["Content-Type"] = "text/html";
            return content;
        }

        public static string Scan(string jsonpath, HttpContext contex)
        {

            var info = JsonConvert.DeserializeObject<MainThing>(System.IO.File.ReadAllText(jsonpath));
            string content = "";
            content += info.scan.scanTime + "<br>";
            content += info.scan.db + "<br>";
            content += info.scan.server + "<br>";
            content += info.scan.errorCount.ToString() + "<br>";
            return content;
        }

        public static string ErrorsCount(string jsonpath, HttpContext contex)
        {

            var info = JsonConvert.DeserializeObject<MainThing>(System.IO.File.ReadAllText(jsonpath));
            string content = "";
            content += info.scan.errorCount.ToString() + "<br>";
            return content;
        }

        public static string Filename(string jsonpath, HttpContext contex,bool correct)
        {

            var info = JsonConvert.DeserializeObject<MainThing>(System.IO.File.ReadAllText(jsonpath));
            string content = "";
            foreach (var item in info.files)
            {
                if (item.result==correct)
                {
                    content += item.filename + "<br>";
                }
            }
            return content;
        }

        public static string Errors(string jsonpath, HttpContext contex)
        {
            
            var info = System.Text.Json.JsonSerializer.Deserialize<MainThing>(System.IO.File.ReadAllText(jsonpath));
            string content = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset = utf-8\"/>";
           // string content = "";
            foreach (var item in info.files)
            {
                if(!item.result)
                {
                    foreach (var error in item.errors)
                    {
                        content += error.error + "<br>";
                    }
                }
            }
            return content;
        }

    }
}
