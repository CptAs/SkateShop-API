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
        private static Random rng;

        public ProductService(MyDbContext context, IProducerService proService, ICategoryService catService)
        {
            _context = context;
            rng = new Random();
        }
        public async Task<Product> CreateProduct(string name, decimal price, string description, int discount, int categoryId, int producerid, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
            if (product != null)
            {
                return null;
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
            var product = await _context.Products.AsNoTracking().Include(x => x.Category).Include(x => x.Producer).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
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
            var product = await _context.Products.AsNoTracking().Include(x => x.Category).Include(x => x.Producer).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
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

        public async Task<IEnumerable<Product>> GetProductsFromCategory(int id, CancellationToken cancellationToken)
        {
            var products = await _context.Products.Include(x => x.Category).Include(x => x.Producer).Where(x => x.Category.CategoryId == id).ToListAsync(cancellationToken);
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsFromProducer(int id, CancellationToken cancellationToken)
        {
            var products = await _context.Products.Include(x => x.Category).Include(x => x.Producer).Where(x => x.Producer.ProducerId == id).ToListAsync(cancellationToken);
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsWithDiscount(CancellationToken cancellationToken = default)
        {
            var products = await _context.Products.Include(x => x.Category).Include(x => x.Producer).Where(x => x.Discount != 0).ToListAsync(cancellationToken);
            return products.OrderByDescending(x => x.Discount).Take(10);
        }
        public async Task<IEnumerable<Product>> GetTenRandomProductsFromCategory(int id, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.Include(x => x.Category).Include(x => x.Producer).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if(product == null)
            {
                return new List<Product>();
            }
            var products = await _context.Products.Include(x => x.Category).Include(x => x.Producer).Where(x => x.Category.CategoryId == product.Category.CategoryId && x.Id != id).ToListAsync(cancellationToken);
            return products.OrderBy(a => rng.Next()).Take(10);
        }

        public async Task<IEnumerable<Product>> GetProductsFromListOfIds(List<int> ids, CancellationToken cancellationToken = default)
        {
            List<Product> products = new List<Product>();
            foreach(int id in ids)
            {
                var product = await _context.Products.Include(x => x.Category).Include(x => x.Producer).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
                if(product!=null)
                {
                    products.Add(product);
                }
            }
            return products;
        }

        public async Task<IEnumerable<Product>> GetBestsellingProducts(CancellationToken cancellationToken = default)
        {
            var checkDate = DateTime.Now.AddDays(-30);
            var orderProducts = await _context.Orders
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Producer)
                .Where(x => x.OrderDate >= checkDate)
                .SelectMany(x => x.OrderProducts)
                .Select(x => x.Product)
                .ToListAsync(cancellationToken);
            Dictionary<Product, int> products = new Dictionary<Product, int>();
            foreach(Product product in orderProducts)
            {
                if(products.ContainsKey(product))
                {
                    products[product]++;
                }
                else
                {
                    products[product] = 1;
                }
            }
            return products.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value).Keys.Take(10);
        }

        public async Task<IEnumerable<Product>> FillProduct(CancellationToken cancellationToken = default)
        {
            var products = new List<Product>();
            var categories = await _context.Categories.ToListAsync(cancellationToken);
            var producers = await _context.Producers.ToListAsync(cancellationToken);
            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            string[] names = { "name", "good", "super", "product", "best", "worst", "crazy", "tool", "skate", "board", "nice", "move", "lorem", "ipsum", "dance" };
            for (int i = 0; i < 100; i++)
            {
                int getRanNum1 = rng.Next(names.Length);
                int getRanNum2 = rng.Next(names.Length);
                while (getRanNum2 == getRanNum1)
                    getRanNum2 = rng.Next(names.Length);
                string name = names[getRanNum1] + " " + names[getRanNum2];
                decimal price = Convert.ToDecimal(rng.Next(50000, 150000) / 100.00);
                int discount = rng.Next(-100, 50);
                if(discount < 0)
                {
                    discount = 0;
                }
                int catNum = categories[rng.Next(categories.Count)].CategoryId;
                int prodNum = producers[rng.Next(producers.Count)].ProducerId;
                var product = CreateProduct(name, price, description, discount, catNum, prodNum, cancellationToken);
                if(product.Result!=null)
                {
                    products.Add(product.Result);
                }
            }
            return products;
        }
    }
}
