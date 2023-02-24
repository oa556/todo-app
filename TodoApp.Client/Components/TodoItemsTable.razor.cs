using Microsoft.AspNetCore.Components;
using TodoApp.Shared.Models;
using TodoApp.Shared.Requests;

namespace TodoApp.Client.Components;

public partial class TodoItemsTable
{
    [Parameter]
    public required TodoItemDto[] Items { get; set; }
    [Parameter]
    public required EventCallback<Guid> OnDeletedEventCallback { get; set; }
    [Parameter]
    public required EventCallback<UpdateTodoItemRequest> OnUpdatedEventCallback { get; set; }
}
