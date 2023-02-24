using MediatR;
using TodoApp.Shared.Models;

namespace TodoApp.Api.Application.Commands.CreateTodoItem;

public record CreateTodoItemCommand(string Title,
                                    string Author,
                                    string Url)
    : IRequest<TodoItemDto>;
