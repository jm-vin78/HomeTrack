using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Extensions
{
    public static class WaterReportsExtensions
    {
        public static async Task<WaterReportEntity> GetLastFlatReport(this DbSet<WaterReportEntity> waterReports, long flatId)
        {
            return await waterReports
                .Where(x => x.FlatId == flatId)
                .OrderByDescending(x => x.Date)
                .FirstOrDefaultAsync();
        }
    }
}
