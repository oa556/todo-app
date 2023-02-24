namespace TodoApp.Api.Domain;

public interface IReadOnlyTodoDbContext
{
    IQueryable<TodoItem> TodoItems { get; }
}
