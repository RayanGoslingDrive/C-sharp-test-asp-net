using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using WebApplication4.Data.Interfaces;
using WebApplication4.Data.Models;
using System.Net;

namespace WebApplication4.Data.Controllers
{
    public class HomeController : Controller
    {

        public _Interface _interface;

        public HomeController(_Interface i_interface)
        {
            _interface = i_interface;
        }


           

        public IActionResult Index()
        {

            var webClient = new WebClient();
            var json = webClient.DownloadString(@"C:\Users\vsh21\source\repos\WebApplication4\WebApplication4\data2.json");
            var info = JsonConvert.DeserializeObject<scanInfo>(json);
            ViewBag.db = info.db;
            return View(info);

            //return View();
        }
    }
}
