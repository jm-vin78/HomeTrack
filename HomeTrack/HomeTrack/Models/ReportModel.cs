using DAL.Entities;
using System;

namespace HomeTrack.Models
{
    public class ReportModel
    {
        public DateTime Date { get; set; }

        public int ColdWater { get; set; }

        public int HotWater { get; set; }

        public ReportModel(WaterReportEntity entity)
        {
            Date = entity.Date;
            ColdWater = entity.ColdWater;
            HotWater = entity.HotWater;
        }
    }
}
