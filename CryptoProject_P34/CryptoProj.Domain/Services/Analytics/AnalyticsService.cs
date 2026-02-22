using CryptoProj.Domain.Abstractions;
using CryptoProj.Domain.Exceptions;
using CryptoProj.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoProj.Domain.Services.Analytics
{
    public class AnalyticsService
    {
        private readonly IAnalyticsRepository _analyticsRepository;
        private readonly IDistributedCache _distributedCache;
        private readonly DistributedCacheEntryOptions _distributedCacheEntryOptions;

        public AnalyticsService(IAnalyticsRepository analyticsRepository,
            IDistributedCache distributedCache)
        {
            _analyticsRepository = analyticsRepository;
            _distributedCache = distributedCache;
            _distributedCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };
        }

        public async Task<AnalyticsItem?> Get(int analyticsItemId)
        {
            var cacheKey = GetCacheKey(analyticsItemId);

            var cachedData = await _distributedCache.GetStringAsync(cacheKey);

            if (cachedData != "null" &&
                cachedData is not null)
            {
                await _distributedCache.RefreshAsync(cacheKey);
                return JsonSerializer.Deserialize<AnalyticsItem>(cachedData);
            }


            var analyticsItemFromDB = await _analyticsRepository.Get(analyticsItemId);
            
            if (analyticsItemFromDB is null)
            {
                throw new NotFoundWithIdException(analyticsItemId);
            }
            
            await _distributedCache.SetStringAsync(cacheKey,
                JsonSerializer.Serialize(analyticsItemFromDB),
                _distributedCacheEntryOptions);

            return analyticsItemFromDB;
        }

        public async Task<AnalyticsItem> Add(AnalyticsItem analyticsItem)
        {
            return await _analyticsRepository.Add(analyticsItem);
        }

        public async Task<AnalyticsItem> Update(AnalyticsItem analyticsItem)
        {
            var cacheKey = GetCacheKey(analyticsItem.Id);

            await _distributedCache.RemoveAsync(cacheKey);

            return await _analyticsRepository.Update(analyticsItem);
        }

        private string GetCacheKey(int analyticsItemId) => $"AnalyticsItem_{analyticsItemId}";
    }
}