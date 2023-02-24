using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TodoApp.Api.Domain;

namespace TodoApp.Api.Infrastructure;

public static class DbInitializer
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
        try
        {
            dbContext.Database.EnsureCreated();
            if (dbContext.TodoItems.Any())
            {
                return;
            }
            dbContext.TodoItems.AddRange(CreateTodoItems());
            dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider
                .GetRequiredService<ILoggerFactory>()
                .CreateLogger(nameof(DbInitializer));
            logger.LogError(ex, "An error occured while creating the database");
        }
    }

    private static IEnumerable<TodoItem> CreateTodoItems()
    {
        for (var i = 0; i < 5; i++)
        {
            yield return TodoItem.Create(
                title: $"Very very long long item title {i}",
                url: "https://www.google.com/",
                author: $"Author {i}"
            );
        }
    }
}
