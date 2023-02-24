using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TodoApp.Shared.Models;
using TodoApp.Shared.Requests;

namespace TodoApp.Client.Components;

public partial class TodoItem
{
    [Inject]
    public required IJSRuntime JsRuntime { get; set; }

    [Parameter]
    public required TodoItemDto Item { get; set; }
    [Parameter]
    public required EventCallback<Guid> OnDeletedEventCallback { get; set; }
    [Parameter]
    public required EventCallback<UpdateTodoItemRequest> OnUpdatedEventCallback { get; set; }

    private async Task OnDeletedAsync(Guid id)
    {
        bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm",
            $"Are you sure you want to delete {Item.Title}");
        if (confirmed)
        {
            await OnDeletedEventCallback.InvokeAsync(id);
        }
    }

    private async Task OnCompletedAsync(Guid id)
    {
        await OnUpdatedEventCallback.InvokeAsync(new UpdateTodoItemRequest(id, TodoItemStatus.Completed));
    }

    private async Task OnUncompletedAsync(Guid id)
    {
        await OnUpdatedEventCallback.InvokeAsync(new UpdateTodoItemRequest(id, TodoItemStatus.Todo));
    }
}
