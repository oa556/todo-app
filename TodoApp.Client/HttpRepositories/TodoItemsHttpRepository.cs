using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Json;
using TodoApp.Shared.Models;
using TodoApp.Shared.Requests;
using TodoApp.Shared.Responses;

namespace TodoApp.Client.HttpRepositories;

public class TodoItemsHttpRepository : ITodoItemsHttpRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public TodoItemsHttpRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<GetAllTodoItemsResponse> GetAsync(GetAllTodoItemsRequest request)
    {
        var queryStringParameters = new Dictionary<string, string>
        {
            [nameof(request.Status)] = request.Status.HasValue ? ((byte)request.Status).ToString() : string.Empty,
            [nameof(request.CurrentPage)] = request.CurrentPage.ToString(),
            [nameof(request.PageSize)] = request.PageSize.ToString()
        };
        HttpResponseMessage response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("api/todo", queryStringParameters));
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        GetAllTodoItemsResponse itemsResponse = JsonSerializer.Deserialize<GetAllTodoItemsResponse>(content, _options)
            ?? throw new ApplicationException(content);
        return itemsResponse;
    }

    public async Task CreateAsync(CreateTodoItemRequest request)
    {
        var content = JsonSerializer.Serialize(request);
        var body = new StringContent(content, Encoding.UTF8, "application/json");

        var postResult = await _httpClient.PostAsync("api/todo", body);

        if (!postResult.IsSuccessStatusCode)
        {
            var postContent = await postResult.Content.ReadAsStringAsync();
            throw new ApplicationException(postContent);
        }
    }

    public async Task UpdateAsync(UpdateTodoItemRequest request)
    {
        var content = JsonSerializer.Serialize(request);
        var body = new StringContent(content, Encoding.UTF8, "application/json");

        var putResult = await _httpClient.PutAsync("api/todo", body);

        if (!putResult.IsSuccessStatusCode)
        {
            var postContent = await putResult.Content.ReadAsStringAsync();
            throw new ApplicationException(postContent);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var url = Path.Combine("api/todo", id.ToString());

        var deleteResult = await _httpClient.DeleteAsync(url);
        var deleteContent = await deleteResult.Content.ReadAsStringAsync();

        if (!deleteResult.IsSuccessStatusCode)
        {
            throw new ApplicationException(deleteContent);
        }
    }
}
