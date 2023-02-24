using MediatR;
using TodoApp.Shared.Models;
using TodoApp.Shared.Responses;

namespace TodoApp.Api.Application.Queries.GetAllTodoItems;

public record GetAllTodoItemsQuery(TodoItemStatus? Status,
                                   int CurrentPage,
                                   int PageSize)
    : IRequest<GetAllTodoItemsResponse>;
