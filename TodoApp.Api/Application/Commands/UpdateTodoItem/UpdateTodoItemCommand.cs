using MediatR;
using TodoApp.Shared.Models;

namespace TodoApp.Api.Application.Commands.UpdateTodoItem;

public record UpdateTodoItemCommand(Guid Id,
                                    TodoItemStatus Status)
    : IRequest<TodoItemDto>;
