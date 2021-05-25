using AdvancedWebTechnologies.Data;
using AdvancedWebTechnologies.Entities;
using AdvancedWebTechnologies.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Services
{
    public class OrderService : IOrderService
    {
        private readonly MyDbContext _context;
        public OrderService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrder(User user, CancellationToken cancellationToken = default)
        {
            var order = new Order(user);
            _context.Add(order);
            await _context.SaveChangesAsync(cancellationToken);
            return order;
        }

        public async Task<Order> CreateOrderFromList(User user, List<List<int>> orderProducts, CancellationToken cancellationToken = default)
        {
            var order = new Order(user);
            decimal price = 0;
            foreach(List<int> orderData in orderProducts)
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == orderData[0], cancellationToken);
                if(product!=null)
                {
                    var orderProduct = new OrderProduct(orderData[1], product, order);
                    price += orderProduct.Quantity * (orderProduct.Product.Price * (decimal)(100 - orderProduct.Product.Discount)/100);
                    _context.Add(orderProduct);
                }
            }
            order.SumPrice = price;
            _context.Add(order);
            await _context.SaveChangesAsync(cancellationToken);
            return order;

        }

        public async Task<OrderProduct> CreateOrderProduct(int quantity, int orderId, int productId, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == orderId, cancellationToken);
            if(product == null || order == null)
            {
                return null;
            }
            var orderProduct = await _context.OrderProducts.AsNoTracking().FirstOrDefaultAsync(x => x.Product == product || x.Order == order || x.Quantity == quantity, cancellationToken);
            if(orderProduct!=null)
            {
                return null;
            }
            orderProduct = new OrderProduct(quantity, product, order);
            _context.Add(orderProduct);
            _context.Attach(order);
            order.SumPrice = order.SumPrice + (product.Price * quantity);
            await _context.SaveChangesAsync(cancellationToken);
            return orderProduct;
        }

        public async Task<Order> DeleteOrder(int id, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.Include(x => x.OrderProducts).FirstOrDefaultAsync(x => x.OrderId == id, cancellationToken);
            if(order == null)
            {
                return null;
            }
            foreach(var orderItem in order.OrderProducts)
            {
                _context.Attach(orderItem);
                _context.Entry(orderItem).State = EntityState.Deleted;
            }
            _context.Attach(order);
            _context.Entry(order).State = EntityState.Deleted;
            await _context.SaveChangesAsync(cancellationToken);
            return order;
        }

        public async Task<OrderProduct> DeleteOrderProduct(int id, CancellationToken cancellationToken = default)
        {
            var orderProduct = await _context.OrderProducts.FirstOrDefaultAsync(x => x.OrderProductID == id, cancellationToken);
            if (orderProduct == null)
            {
                return null;
            }
            _context.Attach(orderProduct);
            _context.Entry(orderProduct).State = EntityState.Deleted;
            await _context.SaveChangesAsync(cancellationToken);
            return orderProduct;
        }

        public async Task<Order> GetOrderById(int id, CancellationToken cancelationToken = default)
        {
            var order = await _context.Orders.Include(x => x.OrderProducts).FirstOrDefaultAsync(x => x.OrderId == id, cancelationToken);
            return order;
        }

        public async Task<OrderProduct> GetOrderProductById(int id, CancellationToken cancellationToken = default)
        {
            var orderProduct = await _context.OrderProducts.FirstOrDefaultAsync(x => x.OrderProductID == id, cancellationToken);
            return orderProduct;
        }

        public async Task<IEnumerable<Order>> GetOrders(CancellationToken cancellationToken = default)
        {
            var orders = await _context.Orders.Include(x=>x.User).Include(x => x.OrderProducts).ThenInclude(x => x.Product).ToListAsync(cancellationToken);
            return orders;
        }

        public async Task<Order> UpdateOrder(int id, Status status, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.OrderId == id, cancellationToken);
            if(order == null)
            {
                return null;
            }
            _context.Attach(order);
            order.Status = status;
            await _context.SaveChangesAsync(cancellationToken);
            return order;
        }

        public async Task<OrderProduct> UpdateOrderProduct(int id, int quantity, CancellationToken cancellationToken = default)
        {
            var orderProduct = await _context.OrderProducts.AsNoTracking().FirstOrDefaultAsync(x => x.OrderProductID == id);
            if(orderProduct == null)
            {
                return null;
            }
            _context.Attach(orderProduct);
            orderProduct.Quantity = quantity;
            await _context.SaveChangesAsync(cancellationToken);
            return orderProduct;
        }
    }
}
