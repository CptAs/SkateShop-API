using AdvancedWebTechnologies.Data;
using AdvancedWebTechnologies.Interfaces;
using AdvancedWebTechnologies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AdvancedWebTechnologies.Services
{
    class StatisticsService : IStaticticsService
    {
        private readonly MyDbContext _context;

        public StatisticsService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<int, decimal>> GetStatisticsFromLastMonth(CancellationToken cancellationToken = default)
        {
            var today = DateTime.Now;
            Dictionary<int, decimal> statistics = new Dictionary<int, decimal>();
            var dateFirstToCheck = new DateTime(today.AddMonths(-1).Year, today.AddMonths(-1).Month, 1);
            while(dateFirstToCheck.Month == today.AddMonths(-1).Month)
            {
                var orders = await _context.Orders.Where(x => x.OrderDate.Date == dateFirstToCheck.Date).ToListAsync(cancellationToken);
                var totalPrice = orders.Sum(order => order.SumPrice);
                statistics.Add(dateFirstToCheck.Day, totalPrice);
                dateFirstToCheck = dateFirstToCheck.AddDays(1);
            }
            return statistics;

        }

        public Task<Dictionary<string, decimal>> GetStatisticsFromLastYear(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<int, decimal>> GetStatisticsFromThisMonth(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, decimal>> GetStatisticsFromThisYear(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
