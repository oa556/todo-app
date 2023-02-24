using Microsoft.AspNetCore.Components;
using TodoApp.Client.HttpRepositories;
using TodoApp.Client.Shared;
using TodoApp.Shared.Models;
using TodoApp.Shared.Requests;
using TodoApp.Shared.Responses;

namespace TodoApp.Client.Components;

public partial class TodoItems
{
	[Inject]
	public required ITodoItemsHttpRepository Repository { get; set; }
	[Parameter]
	public required TodoItemStatus Status { get; set; }

	private TodoItemDto[]? Items { get; set; }
	private MetaData? MetaData { get; set; }
	private SuccessNotification _notification = null!;

	protected override async Task OnInitializedAsync()
	{
		await GetAsync();
	}

	private async Task OnPageSelected(int pageNumber)
	{
		await GetAsync(new GetAllTodoItemsRequest { Status = Status, CurrentPage = pageNumber });
	}

	private async Task GetAsync()
	{
		await GetAsync(new GetAllTodoItemsRequest { Status = Status });
	}

	private async Task GetAsync(GetAllTodoItemsRequest request)
	{
		GetAllTodoItemsResponse response = await Repository.GetAsync(request);
		Items = response.Items;
		MetaData = response.MetaData;
	}

	private async Task DeleteAsync(Guid id)
	{
		await Repository.DeleteAsync(id);
		await GetAsync();
	}

	private async Task UpdateAsync(UpdateTodoItemRequest request)
	{
		await Repository.UpdateAsync(request);
		await GetAsync();
		RedirectWithNotification();
	}

	private void RedirectWithNotification()
	{
		if (Status == TodoItemStatus.Completed)
		{
			_notification.Show("/completed");
		}
		else
		{
			_notification.Show();
		}
	}
}
