namespace TodoApp.Api.Domain;

public interface ITodoItemsRepository
{
    Task<TodoItem?> FindAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(TodoItem item, CancellationToken cancellationToken);
    Task CompleteAsync(TodoItem item, CancellationToken cancellationToken);
    Task UncompleteAsync(TodoItem item, CancellationToken cancellationToken);
    Task DeleteAsync(TodoItem item, CancellationToken cancellationToken);
}
