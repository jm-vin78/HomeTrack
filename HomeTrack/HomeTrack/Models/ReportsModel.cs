using DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTrack.Models
{
    public class ReportsModel
    {
        public string UserName { get; set; }

        public long? FlatId { get; private set; }

        public List<ReportModel> Reports { get; } = new List<ReportModel>(0);

        public List<UserFlatModel> UserFlats { get; } = new List<UserFlatModel>(0);

        public async Task LoadAsync(AppDbContext context, long userId, string userName, long? flatId = null)
        {
            FlatId = flatId;
            UserName = userName;

            if (!FlatId.HasValue)
            {
                FlatId = await context.FlatUsers
                    .Where(x => x.UserId == userId)
                    .Select(x => x.FlatId)
                    .FirstOrDefaultAsync();
            }

            var reports = await context.WaterReports
                .Where(x => x.FlatId == FlatId)
                .Select(x => new ReportModel(x))
                .ToListAsync();

            Reports.AddRange(reports);

            var userFlats = await context.FlatUsers
                .Where(x => x.UserId == userId)
                .Select(x => new UserFlatModel(x))
                .ToListAsync();

            UserFlats.AddRange(userFlats);
        }
    }
}
