using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using static CRM.Controllers.OrdersController;

namespace CRM
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DeliveryType
    {
        Fast,
        Standart,
        Express,
    }
    public class Order
    {
        public int Id { get; set;}
        public string Status { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public double DeliveryCost { get; set; }

        public int Сustomer { get;set;}

        public double Distance { get; set;}
        public double Value { get; set; }

        public int ProductID { get; set; }
        
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }

        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }
        
    }
}
