using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("flat_users")]
    public class FlatUserEntity
    {
        [Column("user_id")]
        public long UserId { get; set; }

        public virtual UserEntity User { get; set; }

        [Column("flat_id")]
        public long FlatId { get; set; }

        public virtual FlatEntity Flat { get; set; }
    }
}
