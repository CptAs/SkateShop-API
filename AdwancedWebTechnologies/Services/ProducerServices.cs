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
    public class ProducerServices : IProducerService
    {
        private readonly MyDbContext _context;

        public ProducerServices(MyDbContext context)
        {
            _context = context;
        }
        public async Task<Producer> CreateAsync(string name, CancellationToken cancelationToken = default)
        {
            var prod = await _context.Producers.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name, cancelationToken);
            if (prod!=null)
            {
                throw new EntityAlreadyExistsException("Producer already exists");
            }
            Producer producer = new Producer(name);
            _context.Add(producer);
            await _context.SaveChangesAsync(cancelationToken);
            return producer;
        }

        public async Task<Producer> DeleteProducerAsync(int id, CancellationToken cancellationToken = default)
        {
            var prod = await _context.Producers.AsNoTracking().FirstOrDefaultAsync(x => x.ProducerId == id, cancellationToken);
            if (prod == null)
            {
                return null;
            }
            _context.Attach(prod);
            _context.Entry(prod).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return prod;
        }

        public async Task<Producer> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var prod = await _context.Producers.AsNoTracking().FirstOrDefaultAsync(x => x.ProducerId == id);
            return prod;
        }

        public async Task<IEnumerable<Producer>> GetProducers(CancellationToken cancellationToken = default)
        {
            var prod = await _context.Producers.ToListAsync(cancellationToken);
            return prod;
        }

        public async Task<Producer> UpdateProducerAsync(int id, string name, CancellationToken cancellationToken = default)
        {
            var prod = await _context.Producers.AsNoTracking().FirstOrDefaultAsync(x => x.ProducerId == id);
            if (prod == null)
            {
                return null;
            }
            _context.Attach(prod);
            if (!string.IsNullOrWhiteSpace(name))
            {
                prod.Name = name;
            }
            await _context.SaveChangesAsync(cancellationToken);

            return prod;
        }
    }
}
