using TodoApp.Shared.Requests;
using TodoApp.Shared.Responses;

namespace TodoApp.Client.HttpRepositories;

public interface ITodoItemsHttpRepository
{
    Task<GetAllTodoItemsResponse> GetAsync(GetAllTodoItemsRequest request);
    Task CreateAsync(CreateTodoItemRequest request);
    Task UpdateAsync(UpdateTodoItemRequest request);
    Task DeleteAsync(Guid id);
}
