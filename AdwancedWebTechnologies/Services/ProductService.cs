using AdvancedWebTechnologies.Data;
using AdvancedWebTechnologies.Entities;
using AdvancedWebTechnologies.Interfaces;
using Amazon.DirectoryService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Services
{
    public class ProductService : IProductService
    {
        private readonly MyDbContext _context;

        public ProductService(MyDbContext context, IProducerService proService, ICategoryService catService)
        {
            _context = context;
        }
        public async Task<Product> CreateProduct(string name, decimal price, string description, int discount, int categoryId, int producerid, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
            if (product != null)
            {
                throw new EntityAlreadyExistsException("Category already exists");
            }
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId, cancellationToken);
            var producer = await _context.Producers.FirstOrDefaultAsync(x => x.ProducerId == producerid, cancellationToken);
            if(category==null || producer == null)
            {
                return null;
            }
            Product p = new Product(name, price, description, discount, category, producer);
            _context.Add(p);
            await _context.SaveChangesAsync(cancellationToken);
            return p;
        }

        public async Task<Product> DeleteProduct(int id, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (product == null)
            {
                return null;
            }
            _context.Attach(product);
            _context.Entry(product).State = EntityState.Deleted;
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken = default)
        {
            var products = await _context.Products.Include(x => x.Category).Include(x => x.Producer).ToListAsync(cancellationToken);
            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id, CancellationToken cancelationToken = default)
        {
            var product = await _context.Products.AsNoTracking().Include(x => x.Producer).Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id, cancelationToken);
            return product;
        }

        public async Task<Product> UpdateProduct(int id, string name, decimal price, string description, int discount, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (product == null)
            {
                return null;
            }
            _context.Attach(product);
            if (!string.IsNullOrWhiteSpace(name))
            {
                product.Name = name;
            }
            if (!string.IsNullOrWhiteSpace(description))
            {
                product.Description = description;
            }
            if (price > 0)
            {
                product.Price = price;
            }
            product.Discount = discount;
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }
    }
}
