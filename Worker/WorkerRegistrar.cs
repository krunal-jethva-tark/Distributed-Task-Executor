using DTS.Models.Worker;

namespace Worker;

public class WorkerRegistrar
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<WorkerRegistrar> _logger;
    private readonly string _allocatorUri;

    public WorkerRegistrar(IConfiguration configuration, ILogger<WorkerRegistrar> logger, IHostApplicationLifetime appLifetime)
    {
        _configuration = configuration;
        _logger = logger;
        _allocatorUri = configuration.GetValue<string>("AllocatorUri");
        appLifetime.ApplicationStopping.Register(UnRegisterWorker);
    }

    public WorkerInfo GetWorkerInfo()
    {
        return new WorkerInfo(
            _configuration.GetValue<string>("name"),
            _configuration.GetValue<int>("port")
        );
    }
    
    public async Task RegisterWorkerAsync()
    {
        var worker = GetWorkerInfo();
        
        var client = new HttpClient();
        var response = await client.PostAsJsonAsync($"{_allocatorUri}/api/nodes/register", new
        {
            worker.Name,
            Address = $"http://localhost:{worker.Port}"
        });
        response.EnsureSuccessStatusCode();
        
        _logger.LogInformation("Worker registered with name: {WorkerName}", worker.Name);
    }

    public void UnRegisterWorker()
    {
        var worker = GetWorkerInfo();
        _logger.LogInformation("Worker un-registered with name: {WorkerName}", worker.Name);
        var client = new HttpClient();
        _ = client.DeleteAsync(worker.Name);

    }
}