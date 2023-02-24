using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Domain;
using TodoApp.Shared.Models;
using TodoApp.Shared.Responses;

namespace TodoApp.Api.Application.Queries.GetAllTodoItems;

public class GetAllTodoItemsQueryHandler : IRequestHandler<GetAllTodoItemsQuery, GetAllTodoItemsResponse>
{
    private readonly IReadOnlyTodoDbContext _dbContext;

    public GetAllTodoItemsQueryHandler(IReadOnlyTodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetAllTodoItemsResponse> Handle(GetAllTodoItemsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<TodoItem> query = _dbContext.TodoItems;

        query = Filter(query, request);
        int totalCount = await query.CountAsync(cancellationToken);
        MetaData metaData = new(request.CurrentPage, request.PageSize, totalCount);

        query = Sort(query, request);
        query = Paginate(query, request);
        TodoItem?[] todoItems = await query.ToArrayAsync(cancellationToken);

        return new GetAllTodoItemsResponse(todoItems.Adapt<TodoItemDto[]>(), metaData);
    }

    private static IQueryable<TodoItem> Filter(IQueryable<TodoItem> query, GetAllTodoItemsQuery request)
    {
        if (request.Status is TodoItemStatus status)
        {
            return query.Where(i => i.Status == status);
        }
        return query;
    }

    private static IQueryable<TodoItem> Sort(IQueryable<TodoItem> query, GetAllTodoItemsQuery request)
    {
        if (request.Status is TodoItemStatus.Completed)
        {
            return query.OrderByDescending(i => i.DateCompleted);
        }
        return query.OrderByDescending(i => i.DateAdded);
    }

    private static IQueryable<TodoItem> Paginate(IQueryable<TodoItem> query, GetAllTodoItemsQuery request)
    {
        return query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize);
    }
}
