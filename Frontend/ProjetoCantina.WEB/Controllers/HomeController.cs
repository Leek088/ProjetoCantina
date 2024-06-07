using Microsoft.AspNetCore.Mvc;
using ProjetoCantina.WEB.Models;
using System.Diagnostics;

namespace ProjetoCantina.WEB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.HomeIndex = true;
            return View();
        }
    }
}
