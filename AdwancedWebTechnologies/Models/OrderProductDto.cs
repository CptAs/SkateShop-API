using AdvancedWebTechnologies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Models
{
    public class OrderProductDto
    {
        public int OrderProductID { get; private set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public OrderProductDto(OrderProduct orderProduct)
        {
            OrderProductID = orderProduct.OrderProductID;
            Product = orderProduct.Product;
            Quantity = orderProduct.Quantity;
        }

    }
}
