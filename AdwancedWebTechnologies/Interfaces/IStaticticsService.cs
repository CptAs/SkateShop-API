using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdvancedWebTechnologies.Interfaces
{
    public interface IStaticticsService
    {
        Task<Dictionary<string, decimal>> GetStatisticsFromThisYear(CancellationToken cancellationToken = default);
        Task<Dictionary<string, decimal>> GetStatisticsFromLastYear(CancellationToken cancellationToken = default);
        Task<Dictionary<int, decimal>> GetStatisticsFromThisMonth(CancellationToken cancellationToken = default);
        Task<Dictionary<int, decimal>> GetStatisticsFromLastMonth(CancellationToken cancellationToken = default);
    }
}
