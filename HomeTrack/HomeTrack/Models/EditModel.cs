using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTrack.Models
{
    public class EditModel
    {
        public long? ReportId { get; set; }

        public long FlatId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Показания холодной воды должны быть заполнены.")]
        [Display(Name = "Холодная вода:")]
        public int ColdWater { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Показания горячей воды должны быть заполнены.")]
        [Display(Name = "Горячая вода:")]
        public int HotWater { get; set; }

        public void Validate(ModelStateDictionary modelState, WaterReportEntity entity)
        {
            if (ColdWater < entity.ColdWater)
                modelState.AddModelError(nameof(ColdWater), "Значение холодной воды должно быть больше, чем уже указанное.");
  
            if (HotWater < entity.HotWater)
                modelState.AddModelError(nameof(ColdWater), "Значение горячей воды должно быть больше, чем уже указанное.");
        }

        public async Task TryLoadAsync(AppDbContext context, long flatId)
        {
            FlatId = flatId;

            var now = DateTime.Now;
            var monthStart = new DateTime(now.Year, now.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            var report = await context.WaterReports
                .Where(x => x.Id == flatId)
                .Where(x => (x.Date > monthStart) && (x.Date < monthEnd))
                .FirstOrDefaultAsync();

            if (report != null)
            {
                ReportId = report.Id;
                ColdWater = report.ColdWater;
                HotWater = report.HotWater;
            }
        }
    }
}
