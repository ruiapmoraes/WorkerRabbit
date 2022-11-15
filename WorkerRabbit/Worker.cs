using WorkerRabbit.Service;

namespace WorkerRabbit
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IReadMessage _readMessage;

        public Worker(ILogger<Worker> logger, IReadMessage readMessage)
        {
            _logger = logger;
            _readMessage = readMessage;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //await Task.Delay(1000, stoppingToken);
                // Executar o método Read
                await Task.Run(() => _readMessage.Read());
            }
        }
    }
}