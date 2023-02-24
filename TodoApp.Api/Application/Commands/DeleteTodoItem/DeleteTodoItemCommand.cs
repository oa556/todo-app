using MediatR;

namespace TodoApp.Api.Application.Commands.DeleteTodoItem;

public record DeleteTodoItemCommand(Guid Id)
    : IRequest;
