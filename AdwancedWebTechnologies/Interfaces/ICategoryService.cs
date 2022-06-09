using AdvancedWebTechnologies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryByIdAsync(int id, CancellationToken cancelationToken = default);
        Task<IEnumerable<Category>> GetCategories(CancellationToken cancellationToken = default);
        Task<Category> CreateCategory(string name, CancellationToken cancellationToken = default);
        Task<Category> DeleteCategory(int id, CancellationToken cancellationToken = default);
        Task<Category> UpdateCategory(int id, string name, CancellationToken cancellationToken = default);
    }
}
