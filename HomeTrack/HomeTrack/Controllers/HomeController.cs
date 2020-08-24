using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeTrack.Models;
using DAL;

namespace HomeTrack.Controllers
{
    public class HomeController : Controller
    {
        public AppDbContextFactory Factory { get; }

        public HomeController(AppDbContextFactory factory)
        {
            Factory = factory;
        }

        public IActionResult Index()
        {
            using (var context = Factory.CreateContext())
            {
                var flats = context.Flats
                    .Where(x => x.Id % 2 == 0)
                    .Where(x => x.Number == 2)
                    .ToList();
                return View(flats);
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
