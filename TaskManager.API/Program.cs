using TaskManager.API.Models;
using TaskManager.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<EmailServerOptions>(
    builder.Configuration.GetSection("ConfiguracionServicios:Email"));

builder.Services.AddSingleton<ITaskService,TaskService>();
builder.Services.AddSingleton<INotifier, MailNotifier>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    var taskService = serviceProvider.GetRequiredService<ITaskService>();
    var emailNotificador = serviceProvider.GetRequiredService<INotifier>();

    taskService.OnChangeStatus += (taskId, newStatus) =>
    {

        var message = new Message<object>
        {
            Addressee = "marco.588@hotmail.com",
            Subject = $"La tarea con ID {taskId} ha cambiado su estado a {newStatus}",
            Data = new { TaskId = taskId, Status = newStatus, ChangeDate = DateTime.UtcNow }

        };

        emailNotificador.Send(message);
    };
}   

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
