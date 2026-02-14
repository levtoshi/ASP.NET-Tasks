using CryptoProj.Domain.Abstractions;
using CryptoProj.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoProj.Storage.Repositories
{
    public class AnalyticsRepository : BaseRepository<AnalyticsItem>, IAnalyticsRepository
    {
        public AnalyticsRepository(CryptoContext context) : base(context)
        {
        }

        public async Task<AnalyticsItem> Update(AnalyticsItem analyticsItem)
        {
            Context.AnalyticsItems.Update(analyticsItem);
            await Context.SaveChangesAsync();
            return analyticsItem;
        }
    }
}