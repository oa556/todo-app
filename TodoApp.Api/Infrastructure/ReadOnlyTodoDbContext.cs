using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Domain;

namespace TodoApp.Api.Infrastructure;

public class ReadOnlyTodoDbContext : IReadOnlyTodoDbContext
{
    private readonly TodoDbContext _todoDbContext;

    public ReadOnlyTodoDbContext(TodoDbContext todoDbContext)
    {
        _todoDbContext = todoDbContext;
    }

    public IQueryable<TodoItem> TodoItems => _todoDbContext.TodoItems.AsNoTracking().AsQueryable();
}
