using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HomeTrack.Models;
using DAL;

namespace HomeTrack.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
