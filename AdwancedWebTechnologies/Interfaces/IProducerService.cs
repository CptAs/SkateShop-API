using AdvancedWebTechnologies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Interfaces
{
    public interface IProducerService
    {
        Task<Producer> CreateAsync(string name, CancellationToken cancelationToken = default);
        Task<Producer> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Producer>> GetProducers(CancellationToken cancellationToken = default);
        Task<Producer> DeleteProducerAsync(int id, CancellationToken cancellationToken = default);
        Task<Producer> UpdateProducerAsync(int id, string name, CancellationToken cancellationToken = default);
    }
}
