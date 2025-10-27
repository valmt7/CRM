using CRM.MidMiddleware;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services;

public class ReportService : IReportService
{
    private readonly AppDbContext _context;

    public ReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Report>> GetAllReportsAsync()
    {
        var reports = await _context.Reports.ToListAsync();
        if (reports.Count == 0)
        {
            throw new NotFoundReportsExeption();
        }
        return reports;
    }

    public async Task<IEnumerable<Report>> GetAllClientReports(int clientId)
    {
        var report = await _context.Reports.Where(x => x.ClientId == clientId).OrderBy(x=>x.CreateDate).ToListAsync();
        if (report.Count == 0)
        {
            throw new NotFoundReportsExeption();
        }
        return report;
    }

    public async Task<double> GetAverageDeliveryTime(int clientId,string filter,DateTime now)
    {
        DateTime DateTimeFilter;
        
        switch (filter)
        {
            case "day":
                DateTimeFilter = now.AddDays(-1); 
                break;
            case "week":
                DateTimeFilter = now.AddDays(-7);
                break;
            case "month":
                DateTimeFilter = now.AddMonths(-1);
                break;
            case "year":
                DateTimeFilter = now.AddYears(-1);
                break;
            default:
                DateTimeFilter = now;
                break;
        }
        var reports = await _context.Reports.Where(x => x.ClientId == clientId)
            .Where(x => x.CreateDate >= DateTimeFilter).ToListAsync();
        if (reports.Count == 0)
        {
            throw new NotFoundReportsExeption();
        }
        double result = reports.Select(x=>x.DeliveryTime).Average();
        return result;
    }
    
}