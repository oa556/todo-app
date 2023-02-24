using MediatR;
using TodoApp.Api.Domain;

namespace TodoApp.Api.Application.Commands.DeleteTodoItem;

public class DeleteTodoItemCommandHandler
    : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly ITodoItemsRepository _repository;

    public DeleteTodoItemCommandHandler(ITodoItemsRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.FindAsync(request.Id, cancellationToken) is not TodoItem item)
        {
            throw new EntityNotFoundException($"Entity with id {request.Id} is not found");
        }

        await _repository.DeleteAsync(item, cancellationToken);
    }
}
