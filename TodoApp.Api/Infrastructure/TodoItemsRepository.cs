using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Domain;

namespace TodoApp.Api.Infrastructure;

public class TodoItemsRepository : ITodoItemsRepository
{
    private readonly TodoDbContext _context;

    public TodoItemsRepository(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<TodoItem?> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.TodoItems.FirstOrDefaultAsync(i => i.Id == id,
                                                            cancellationToken);
    }

    public async Task CreateAsync(TodoItem item, CancellationToken cancellationToken)
    {
        await _context.TodoItems.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task CompleteAsync(TodoItem item, CancellationToken cancellationToken)
    {
        item.Complete();
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UncompleteAsync(TodoItem item, CancellationToken cancellationToken)
    {
        item.Unсomplete();
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TodoItem item, CancellationToken cancellationToken)
    {
        _context.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
