using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("flats")]
    public class FlatEntity
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("number")]
        public int Number { get; set; }

        public virtual ICollection<FlatUserEntity> FlatUsers { get; set; }

        public virtual ICollection<WaterReportEntity> WaterReports { get; set; }
    }
}
