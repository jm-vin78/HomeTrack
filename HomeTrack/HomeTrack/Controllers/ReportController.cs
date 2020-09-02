using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using HomeTrack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeTrack.Controllers
{
    public class ReportController : Controller
    {
        public async Task<IActionResult> Editor([FromServices] AppDbContextFactory factory, long flatId)
        {
            var model = new EditModel();
            using (var context = factory.CreateContext())
            {
                await model.TryLoadAsync(context, flatId);
            }
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Editor([FromServices] AppDbContextFactory factory, EditModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var context = factory.CreateContext())
            {
                if (model.ReportId.HasValue)
                {
                    var entity = await context.WaterReports.Where(x => x.Id == model.ReportId).SingleOrDefaultAsync();
                    if (entity != null)
                    {
                        model.Validate(ModelState, entity);
                        if (!ModelState.IsValid)
                            return View(model);

                        entity.ColdWater = model.ColdWater;
                        entity.HotWater = model.ColdWater;

                        await context.SaveChangesAsync();
                    }
                }
                else
                {
                    var entity = new WaterReportEntity();
                    entity.ColdWater = model.ColdWater;
                    entity.HotWater = model.HotWater;

                    context.WaterReports.Add(entity);
                    await context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
