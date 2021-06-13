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
        Task<Product> CreateProduct(string name,decimal price, string description, int discount, int categoryId, int producerId, CancellationToken cancellationToken = default);
        Task<Product> DeleteProduct(int id, CancellationToken cancellationToken = default);
        Task<Product> UpdateProduct(int id, string name, decimal price, string description, int discount, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetProductsFromCategory(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetProductsFromProducer(int id, CancellationToken cancellationToken= default);
        Task<IEnumerable<Product>> GetProductsWithDiscount(CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetTenRandomProductsFromCategory(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetProductsFromListOfIds(List<int> ids, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetBestsellingProducts(CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> FillProduct(CancellationToken cancellationToken = default);
    }
}
