﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;

using WebApplication4.Data.Models;

namespace WebApplication4.Data
{
    public class Responses
    {
        public static string All(string jsonpath)
        {

            string content = System.IO.File.ReadAllText(jsonpath);
            
            return content;
        }

        public static string Scan(string jsonpath)
        {

            var info = JsonConvert.DeserializeObject<MainThing>(System.IO.File.ReadAllText(jsonpath));
            string content = "";
            content += info.scan.scanTime + "<br>";
            content += info.scan.db + "<br>";
            content += info.scan.server + "<br>";
            content += info.scan.errorCount.ToString() + "<br>";
            return content;
        }

        public static string ErrorsCount(string jsonpath)
        {

            var info = JsonConvert.DeserializeObject<MainThing>(System.IO.File.ReadAllText(jsonpath));
            string content = "";
            content += info.scan.errorCount.ToString() + "<br>";
            return content;
        }

        public static string Filename(string jsonpath,bool correct)
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

        public static IEnumerable<Files> Errors(string jsonpath)
        {
            
            var info = System.Text.Json.JsonSerializer.Deserialize<MainThing>(System.IO.File.ReadAllText(jsonpath));
            
            // string content = "";



            var files = from b in info.files where b.result == false
                        select new Files()
                        {

                            filename = b.filename,

                            errors = b.errors,


                        };

            return files;

        }

        public static IEnumerable<QuerryCheck> QuerryCheck(string jsonpath)
        {

            var info = System.Text.Json.JsonSerializer.Deserialize<MainThing>(System.IO.File.ReadAllText(jsonpath));
            string content = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset = utf-8\"/>";
            // string content = "";

            List<string> incorrectFilenames = new List<string>();

           // string Ifilename;

            var Ifilename = from b in info.files where b.result == false && b.filename.Contains("query_") select b.filename;
            string[] Ifilename_this = new string[Ifilename.Count()];
            for (int i = 0; i < Ifilename.Count(); i++)
            {
                Ifilename_this[i] = Ifilename.ElementAt(i);
            }
            var FileCount = from b in info.files where b.filename.Contains("query_") select info.files.Length;
            var FileCountPositive = from b in info.files where b.result == true && b.filename.Contains("query_") select info.files.Length;

            var FileCountNegative = from b in info.files where b.result == false && b.filename.Contains("query_") select info.files.Length;

            var querryChecks =  new QuerryCheck()
                        {

                            TotalFiles = FileCount.Count(),
                            TotalCorrectFiles = FileCountPositive.Count(),
                            TotalIncorrectFiles = FileCountNegative.Count(),
                            IncorrectFilenames = Ifilename_this,


                        };
            yield return querryChecks;
        }

        public static IEnumerable<ServiceInfo> GetServiceInfo()
        {
            var serviceInfo = new ServiceInfo()
            {
                AppName = Assembly.GetExecutingAssembly().FullName,
                Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                DateUtc = DateTime.Now.ToUniversalTime(),
            };

            yield return serviceInfo;
        }


        public static bool AddNewErrors(string data)
        {
            try
            {
                MainThing jsonObject = System.Text.Json.JsonSerializer.Deserialize<MainThing>(data);
                string fileName = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
                System.IO.File.WriteAllText(String.Format("{0}.json", fileName), data);
                return true;
            }
            catch 
            {
                return false;
            }
        }


        public static IEnumerable<Files> Errors_Index(string jsonpath, int i)
        {

            var info = System.Text.Json.JsonSerializer.Deserialize<MainThing>(System.IO.File.ReadAllText(jsonpath));
            string content = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset = utf-8\"/>";
            // string content = "";


            var files = from b in info.files
                        where b.result == false
                        select new Files()
                        {

                            filename = b.filename,

                            errors = b.errors,


                        };
            yield return files.ElementAt(i);
        }
    }
}
