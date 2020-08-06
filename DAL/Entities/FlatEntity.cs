using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    [Table("flats")]
    public class FlatEntity
    {
        [Column("id")]
        public long Id;

        [Column("number")]
        public int Number;

        public virtual ICollection<FlatUserEntity> FlatUsers { get; set; }

        public virtual ICollection<WaterReportEntity> WaterReports { get; set; }
    }
}
