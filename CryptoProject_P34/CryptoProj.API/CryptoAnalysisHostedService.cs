
using CryptoProj.Domain.Abstractions;
using CryptoProj.Domain.Models.Requests;

namespace CryptoProj.API
{
    public class CryptoAnalysisHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CryptoAnalysisHostedService> _logger;

        public CryptoAnalysisHostedService(IServiceProvider serviceProvider,
            ILogger<CryptoAnalysisHostedService> logger)
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

                var request = new CryptocurrencyRequest
                {
                    Limit = 100,
                    Offset = 1,
                };

                while (!cancellationToken.IsCancellationRequested)
                {
                    var crypto = await repo.GetAll(request);

                    var lowCryptos = crypto.Where(c => c.Id < 1000).ToList();

                    _logger.LogInformation("Cryptos with low prices", lowCryptos);

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