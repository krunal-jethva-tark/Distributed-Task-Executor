using DTS.Models.Tasks.TaskType.MemeDownloader;
using DTS.Models.Tasks.TaskType;
using DTS.Models.Worker;
using Worker;
using Worker.BusinessServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<WorkerRegistrar>();
builder.Services.AddSingleton<WorkerInfo>(provider => provider.GetRequiredService<WorkerRegistrar>().GetWorkerInfo());
builder.Services.AddScoped<TaskBusinessService>();
builder.Services.AddScoped<ITaskType, MemeDownloaderTask>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlingMiddleware>();

var provider = builder.Services.BuildServiceProvider();

var worker = provider.GetRequiredService<WorkerInfo>();
app.Urls.Add($"http://0.0.0.0:{worker.Port}");

var registrar = provider.GetRequiredService<WorkerRegistrar>();
await registrar.RegisterWorkerAsync();

Console.WriteLine($"Worker started, listening on port: {worker.Port}. Memes will be saved at: {worker.WorkDir}");

app.Run();