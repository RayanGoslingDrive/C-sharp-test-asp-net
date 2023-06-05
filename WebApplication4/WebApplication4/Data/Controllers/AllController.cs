using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using WebApplication4.Data.Models;

namespace WebApplication4.Data.Controllers
{
    public class AllController : Controller
    {
        
        public IActionResult All()
        {
            var webClient = new WebClient();
            var json = webClient.DownloadString(@"C:\Users\vsh21\source\repos\WebApplication4\WebApplication4\data.json");
            var info = JsonSerializer.Deserialize<scanInfo>(json);
            return View(info);
        }
    }
}
