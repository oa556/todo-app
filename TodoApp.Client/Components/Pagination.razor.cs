using Microsoft.AspNetCore.Components;
using TodoApp.Shared.Models;

namespace TodoApp.Client.Components;

public partial class Pagination
{
    [Parameter]
    public required MetaData? MetaData { get; set; }
    [Parameter]
    public int Spread { get; set; }
    [Parameter]
    public EventCallback<int> PageSelectedEventCallback { get; set; }

    private List<PagingLink>? _links;

    protected override void OnParametersSet()
    {
        CreatePaginationLinks();
    }

    private void CreatePaginationLinks()
    {
        if (MetaData == null)
        {
            return;
        }

        _links = new List<PagingLink>();

        _links.Add(new PagingLink
        {
            Text = "Previous",
            PageNumber = MetaData.CurrentPage - 1,
            IsEnabled = MetaData.HasPrevious
        });

        for (int i = 1; i <= MetaData.TotalPages; i++)
        {
            if (i >= MetaData.CurrentPage - Spread && i <= MetaData.CurrentPage + Spread)
            {
                _links.Add(new PagingLink
                {
                    Text = i.ToString(),
                    PageNumber = i,
                    IsEnabled = true,
                    IsActive = MetaData.CurrentPage == i
                });
            }
        }

        _links.Add(new PagingLink
        {
            Text = "Next",
            PageNumber = MetaData.CurrentPage + 1,
            IsEnabled = MetaData.HasNext
        });
    }

    private async Task OnPageSelected(PagingLink link)
    {
        if (link.PageNumber == MetaData!.CurrentPage || !link.IsEnabled)
        {
            return;
        }

        MetaData = MetaData with { CurrentPage = link.PageNumber };
        await PageSelectedEventCallback.InvokeAsync(link.PageNumber);
    }
}

public class PagingLink
{
    public required string Text { get; init; }
    public required int PageNumber { get; init; }
    public bool IsEnabled { get; init; }
    public bool IsActive { get; init; }
}
