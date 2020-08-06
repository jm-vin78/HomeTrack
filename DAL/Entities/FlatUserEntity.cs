using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    [Table("flat_users")]
    public class FlatUserEntity
    {
        [Column("user_id")]
        public long UserId;

        public virtual UserEntity User { get; set; }

        [Column("flat_id")]
        public long FlatId;

        public virtual FlatEntity Flat { get; set; }
    }
}
