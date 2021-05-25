using AdvancedWebTechnologies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrders(CancellationToken cancellationToken = default);
        Task<Order> GetOrderById(int id, CancellationToken cancelationToken = default);
        Task<Order> DeleteOrder(int id, CancellationToken cancellationToken = default);
        Task<OrderProduct> DeleteOrderProduct(int id, CancellationToken cancellationToken = default);
        Task<Order> UpdateOrder(int id, Status status, CancellationToken cancellationToken = default);
        Task<OrderProduct> UpdateOrderProduct(int id, int quantity, CancellationToken cancellationToken = default);
        Task<Order> CreateOrder(User user, CancellationToken cancellationToken = default);
        Task<OrderProduct> CreateOrderProduct(int quantity, int orderId, int productId, CancellationToken cancellationToken = default);
        Task<OrderProduct> GetOrderProductById(int id, CancellationToken cancellationToken = default);
        Task<Order> CreateOrderFromList(User user, List<List<int>> orderProducts, CancellationToken cancellationToken = default);
 
    }
}
