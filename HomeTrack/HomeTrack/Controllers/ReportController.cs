using System;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using DAL.Extensions;
using HomeTrack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeTrack.Controllers
{
    public class ReportController : Controller
    {
        /// <summary>
        /// Try to get this month's report if it exists
        /// </summary>
        public async Task<IActionResult> Editor([FromServices] AppDbContextFactory factory, long flatId)
        {
            var model = new EditModel();
            using (var context = factory.CreateContext())
            {
                await model.TryLoadAsync(context, flatId);
            }
            return View(model);
        }

        /// <summary>
        /// Update report information for current month or create new report
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Editor([FromServices] AppDbContextFactory factory, EditModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var context = factory.CreateContext())
            {
                var now = DateTime.Now;
                var monthStart = new DateTime(now.Year, now.Month, 1);
                var monthEnd = monthStart.AddMonths(1);

                var report = await context.WaterReports
                    .Where(x => x.FlatId == model.FlatId)
                    .Where(x => (x.Date >= monthStart) && (x.Date < monthEnd))
                    .SingleOrDefaultAsync();

                if (report != null)
                {
                    model.Validate(ModelState, report);
                    if (!ModelState.IsValid)
                        return View(model);

                    report.ColdWater = model.ColdWater;
                    report.HotWater = model.HotWater;

                    await context.SaveChangesAsync();
                }
                else
                {
                    var lastKnownReport = await context.WaterReports.GetLastFlatReport(model.FlatId);
                    model.Validate(ModelState, lastKnownReport);
                    if (!ModelState.IsValid)
                        return View(model);

                    var entity = new WaterReportEntity();
                    entity.FlatId = model.FlatId;
                    entity.ColdWater = model.ColdWater;
                    entity.HotWater = model.HotWater;
                    entity.Date = DateTime.Now;

                    context.WaterReports.Add(entity);
                    await context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index", "Home", new { flatId = model.FlatId });
        }
    }
}
