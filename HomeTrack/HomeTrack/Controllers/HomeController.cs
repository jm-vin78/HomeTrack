using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DAL;
using HomeTrack.Models;
using System.Threading.Tasks;
using HomeTrack.Helpers;

namespace HomeTrack.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index([FromServices] AppDbContextFactory factory, long? flatId)
        {
            var userId = HttpContext.GetCurrentUserId();
            var userName = HttpContext.GetCurrentUserName();
            var model = new ReportsModel();

            using (var context = factory.CreateContext())
            { 
                await model.LoadAsync(context, userId, userName, flatId);
            }

            return View(model);
        }
    }
}
