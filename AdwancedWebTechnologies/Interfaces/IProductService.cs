using AdvancedWebTechnologies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id, CancellationToken cancelationToken = default);
        Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken = default);
        Task<Product> CreateProduct(string name,decimal price, string description, int discount, Category category, Producer producer, CancellationToken cancellationToken = default);
        Task<Product> DeleteProduct(int id, CancellationToken cancellationToken = default);
        Task<Product> UpdateProduct(int id, string name, decimal price, string description, int discount, CancellationToken cancellationToken = default);
    }
}
