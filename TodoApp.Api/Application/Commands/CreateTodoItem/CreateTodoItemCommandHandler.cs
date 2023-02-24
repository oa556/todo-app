using Mapster;
using MediatR;
using TodoApp.Api.Domain;
using TodoApp.Shared.Models;

namespace TodoApp.Api.Application.Commands.CreateTodoItem;

public class CreateTodoItemCommandHandler
    : IRequestHandler<CreateTodoItemCommand, TodoItemDto>
{
    private readonly ITodoItemsRepository _repository;

    public CreateTodoItemCommandHandler(ITodoItemsRepository repository)
    {
        _repository = repository;
    }

    public async Task<TodoItemDto> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        TodoItem item = TodoItem.Create(request.Title, request.Author, request.Url);
        await _repository.CreateAsync(item, cancellationToken);
        return item.Adapt<TodoItemDto>();
    }
}
