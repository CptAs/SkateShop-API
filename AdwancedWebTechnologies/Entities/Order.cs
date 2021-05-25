using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Entities
{
    public enum Status { ORDERD, PAID, SHIPED, FINISHED }
 
    public class Order
    {
        public int OrderId { get; private set; }
        public User? User { get; set; }
        public decimal SumPrice { get; set; }

        public Status Status { get; set; }
        public DateTime OrderDate { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
        public Order() { }
        public Order(User user, decimal sumPrice = 0, Status status = Status.ORDERD)
        {
            User = user;
            SumPrice = sumPrice;
            Status = status;
            OrderDate = DateTime.Now;
        }
    }
}
