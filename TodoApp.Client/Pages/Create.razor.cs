using Microsoft.AspNetCore.Components;
using TodoApp.Client.HttpRepositories;
using TodoApp.Client.Shared;
using TodoApp.Shared.Requests;

namespace TodoApp.Client.Pages;

public partial class Create
{
    [Inject]
    public required ITodoItemsHttpRepository Repository { get; set; }

    private readonly CreateTodoItemRequest _request = new();
    private SuccessNotification _notification = null!;

    private async Task SubmitAsync()
    {
        await Repository.CreateAsync(_request);
        _notification.Show();
    }
}
