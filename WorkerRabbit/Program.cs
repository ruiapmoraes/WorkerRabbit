using WorkerRabbit;
using WorkerRabbit.Service;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IReadMessage, ReadMessage>();
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
