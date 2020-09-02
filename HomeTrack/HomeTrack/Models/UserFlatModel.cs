using DAL.Entities;

namespace HomeTrack.Models
{
    public class UserFlatModel
    {
        public long UserId { get; set; }
        
        public long FlatId { get; set; }

        public UserFlatModel(FlatUserEntity entity)
        {
            UserId = entity.UserId;
            FlatId = entity.FlatId;
        }
    }
}
