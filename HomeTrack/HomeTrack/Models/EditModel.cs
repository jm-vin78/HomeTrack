using DAL;
using DAL.Entities;
using DAL.Extensions;
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
        public long FlatId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Показания холодной воды должны быть заполнены.")]
        [Display(Name = "Холодная вода:")]
        public int ColdWater { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Показания горячей воды должны быть заполнены.")]
        [Display(Name = "Горячая вода:")]
        public int HotWater { get; set; }

        public void Validate(ModelStateDictionary modelState, WaterReportEntity entity)
        {
            if (entity == null)
            {
                return;
            }

            if (ColdWater < entity.ColdWater)
                modelState.AddModelError(nameof(ColdWater), "Значение холодной воды должно быть больше либо равно указанному.");
  
            if (HotWater < entity.HotWater)
                modelState.AddModelError(nameof(HotWater), "Значение горячей воды должно быть больше либо равно указанному.");
        }

        /// <summary>
        /// Try to get last report if it exists
        /// </summary>
        public async Task TryLoadAsync(AppDbContext context, long flatId)
        {
            FlatId = flatId;

            var report = await context.WaterReports.GetLastFlatReport(FlatId);

            if (report != null)
            {
                ColdWater = report.ColdWater;
                HotWater = report.HotWater;
            }
        }
    }
}
