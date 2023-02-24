using System.Text.Json.Serialization;
using TodoApp.Shared.Models;

namespace TodoApp.Shared.Requests;

public sealed class GetAllTodoItemsRequest {
    public int CurrentPage { get; init; } = 1;
    public int PageSize { get; init; } = 5;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TodoItemStatus? Status { get; init; }
};
