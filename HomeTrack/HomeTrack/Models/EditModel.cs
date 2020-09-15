using DAL;
using DAL.Entities;
using DAL.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HomeTrack.Models
{
    public class EditModel
    {
        public long FlatId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "EditModel_ColdWaterRequired")]
        [DisplayName("EditModel_ColdWater")]
        public int ColdWater { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "EditModel_HotWaterRequired")]
        [DisplayName("EditModel_HotWater")]
        public int HotWater { get; set; }

        public void Validate(ModelStateDictionary modelState, WaterReportEntity entity)
        {
            if (entity == null)
            {
                return;
            }

            if (ColdWater < entity.ColdWater)
                modelState.AddModelError(nameof(ColdWater), Resources.Models.EditModel.EditModel_ColdWaterCondition);
  
            if (HotWater < entity.HotWater)
                modelState.AddModelError(nameof(HotWater), Resources.Models.EditModel.EditModel_HotWaterCondition);
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
