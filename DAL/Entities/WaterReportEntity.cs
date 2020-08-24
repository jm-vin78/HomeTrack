using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("water_reports")]
    public class WaterReportEntity
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("flat_id")]
        public long FlatId { get; set; }

        public virtual FlatEntity Flat { get; set; }

        [Column("date")]
        public string Date { get; set; }

        [Column("cold_water")]
        public int ColdWater { get; set; }

        [Column("hot_water")]
        public int HotWater { get; set; }
    }
}
