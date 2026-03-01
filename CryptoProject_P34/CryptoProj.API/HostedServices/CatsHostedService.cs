using CryptoProj.Domain.Abstractions;
using CryptoProj.Domain.Models.Requests;

namespace CryptoProj.API.HostedServices
{
    public class CatsHostedService : IHostedService
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger<CatsHostedService> _logger;

        public CatsHostedService(IServiceProvider serviceProvider,
            ILogger<CatsHostedService> logger)
        {
            _provider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                using var scope = _provider.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<ICatsRepository>();

                var request = new CatsRequest
                {
                    Limit = 100,
                    Offset = 1
                };

                while (!cancellationToken.IsCancellationRequested)
                {
                    var cats = await repo.GetAll(request);

                    if (cats.Count > 0)
                    {
                        Random random = new Random();
                        int index = random.Next(0, cats.Count);

                        var randomCat = cats[index];

                        _logger.LogInformation($"\nRandom cat: {randomCat.Id} | {randomCat.Name}\n");
                    }
                    _logger.LogInformation($"\nNo cats in the database\n");

                    await Task.Delay(5000, cancellationToken);
                }
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Task was cancelled");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}