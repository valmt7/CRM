namespace CRM;
public enum ReportPeriod
{
    day,
    week,
    month,
    year
}
public class Report
{
    public int Id { get; set; }
    public double DeliveryTime { get; set; }
    public double Profit { get; set; }
    public double FleetStatus { get; set; }
    public int RouteId { get; set; }
    public double Spending { get; set; }
    
    public DateTime CreateDate { get; set; }
    public int ClientId { get; set; }
}