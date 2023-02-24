using Mapster;
using MediatR;
using TodoApp.Api.Domain;
using TodoApp.Shared.Models;

namespace TodoApp.Api.Application.Commands.UpdateTodoItem;

public class UpdateTodoItemCommandHandler
    : IRequestHandler<UpdateTodoItemCommand, TodoItemDto>
{
    private readonly ITodoItemsRepository _repository;

    public UpdateTodoItemCommandHandler(ITodoItemsRepository repository)
    {
        _repository = repository;
    }

    public async Task<TodoItemDto> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.FindAsync(request.Id, cancellationToken) is not TodoItem item)
        {
            throw new EntityNotFoundException($"Entity with id {request.Id} is not found");
        }

        if (request.Status == TodoItemStatus.Todo && item.Status != TodoItemStatus.Todo)
        {
            await _repository.UncompleteAsync(item, cancellationToken);
        }
        else if (request.Status == TodoItemStatus.Completed && item.Status != TodoItemStatus.Completed)
        {
            await _repository.CompleteAsync(item, cancellationToken);
        }

        return item.Adapt<TodoItemDto>();
    }
}
