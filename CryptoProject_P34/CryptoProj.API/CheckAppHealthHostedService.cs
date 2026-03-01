
using CryptoProj.Domain.Abstractions;
using CryptoProj.Domain.Models.Requests;
using CryptoProj.Storage;
using System;

namespace CryptoProj.API
{
    public class CheckAppHealthHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CheckAppHealthHostedService> _logger;

        public CheckAppHealthHostedService(IServiceProvider serviceProvider,
            ILogger<CheckAppHealthHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<ICryptocurrencyRepository>();
                var context = scope.ServiceProvider.GetRequiredService<CryptoContext>();

                while (!cancellationToken.IsCancellationRequested)
                {

                    bool canConntect = await context.Database.CanConnectAsync();
                    
                    long totalMemory = GC.GetTotalMemory(false);
                    long memoryInMb = totalMemory / (1024 * 1024);
                    int maxAllowedMb = 256;

                    _logger.LogInformation($"Database can connect status: {canConntect}\n" +
                        $"GC memory amount: {memoryInMb} mb\n" +
                        $"GC is healthy: {memoryInMb <= maxAllowedMb}\n");

                    await Task.Delay(10000, cancellationToken);
                }
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("Error: ", ex.Message);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}