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
    public class CategoryService : ICategoryService
    {
        private readonly MyDbContext _context;

        public CategoryService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategory(string name, int parrentID, CancellationToken cancellationToken = default)
        {
            var cat = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
            if (cat != null)
            {
                throw new EntityAlreadyExistsException("Category already exists");
            }
            var parrent = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == parrentID, cancellationToken);
            Category category;
            if (parrent != null)
            {
                category = new Category(name, parrent);
            }
            else
            {
               category = new Category(name);
            }
            _context.Add(category);
            await _context.SaveChangesAsync(cancellationToken);
            return category;
        }

        public async Task<Category> DeleteCategory(int id, CancellationToken cancellationToken = default)
        {
            var cat = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.CategoryId == id, cancellationToken);
            if(cat == null)
            {
                return null;
            }
            _context.Attach(cat);
            _context.Entry(cat).State = EntityState.Deleted;
            await _context.SaveChangesAsync(cancellationToken);
            return cat;
        }

        public async Task<IEnumerable<Category>> GetCategories(CancellationToken cancellationToken = default)
        {
            var categories = await _context.Categories.AsNoTracking().Include(x => x.Parrent).ToListAsync(cancellationToken);
            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(int id, CancellationToken cancelationToken = default)
        {
            var category = await _context.Categories.AsNoTracking().Include(x=>x.Parrent).FirstOrDefaultAsync(x => x.CategoryId==id, cancelationToken);
            return category;
        }

        public async Task<Category> UpdateCategory(int id, string name, CancellationToken cancellationToken = default)
        {
            var cat = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.CategoryId == id, cancellationToken);
            if (cat == null)
            {
                return null;
            }
            _context.Attach(cat);
            if (!string.IsNullOrWhiteSpace(name))
            {
                cat.Name = name;
            }
            await _context.SaveChangesAsync(cancellationToken);
            return cat;
        }
    }
}
