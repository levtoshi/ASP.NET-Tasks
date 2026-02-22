using CryptoProj.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoProj.Domain.Abstractions
{
    public interface IAnalyticsRepository
    {
        ValueTask<AnalyticsItem?> Get(int analyticsItemId);
        Task<AnalyticsItem> Add(AnalyticsItem analyticsItem);
        Task<AnalyticsItem> Update(AnalyticsItem analyticsItem);
    }
}