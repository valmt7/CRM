namespace CRM.Services;

public interface IReportService
{
    Task<IEnumerable<Report>> GetAllReportsAsync();
    Task<IEnumerable<Report>> GetAllClientReports(int clientId);

    Task<double> GetAverageDeliveryTime(int clientId, ReportPeriod filter, DateTime now);
}