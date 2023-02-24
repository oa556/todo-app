using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApp.Api.Domain;
using TodoApp.Api.Infrastructure;

var host = new HostBuilder()
    .ConfigureServices(services =>
    {
        services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());
        services.AddDbContext<TodoDbContext>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("DbConnectionString"));
        });
        services.AddScoped<IReadOnlyTodoDbContext, ReadOnlyTodoDbContext>();
        services.AddScoped<ITodoItemsRepository, TodoItemsRepository>();
    })
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.CreateDbIfNotExists();

host.Run();
