namespace CRM;

public class Fleet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int OrderId {get; set; }
    public int State { get; set; }
    public string Location { get; set; }
    public int Route { get; set; }
}