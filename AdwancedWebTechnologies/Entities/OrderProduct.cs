using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Entities
{
    public class OrderProduct
    {
        public int OrderProductID { get; private set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public OrderProduct() { }
        public OrderProduct(int quantity, Product product, Order order)
        {
            Quantity = quantity;
            Product = product;
            Order = order;
        }
    }
}
