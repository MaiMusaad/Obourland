using Microsoft.EntityFrameworkCore;
using ObourLand.Entities;
using ObourLand.Models;

namespace ObourLand.Services
{
    public class ReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<ReportDto>> GetReports(int userId)
        {
            return await _context.Reports.Where(w => w.UserId == userId)
                .Include(c => c.User)
                .ThenInclude(c => c.Role)
                .Select(s => new ReportDto
                {
                    UserId = s.UserId,
                    UserName = s.User.UserName,
                    Date = s.Date,
                    Time = s.Time,
                    Comment = s.Comment,
                    X= s.X,
                    Y = s.Y,
                    Image = s.Image,
                    RoleName = s.User.Role.Name,
                    SupervisorId = s.User.SupervisorId,
                    SupervisorName = s.User.Supervisor == null ? " ": $"{s.User.Supervisor.FirstName} {s.User.Supervisor.LastName}"
                })
                .ToListAsync();
        }

        public async Task<int> CreateReport(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return 0;
        }

    }
}
