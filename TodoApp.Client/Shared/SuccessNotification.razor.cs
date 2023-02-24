using Microsoft.AspNetCore.Components;

namespace TodoApp.Client.Shared;

public partial class SuccessNotification
{
    [Inject]
    public required NavigationManager Navigation { get; set; }

    private string? _modalDisplay;
    private string? _modalClass;
    private bool _showBackdrop;
    private string _redirectUrl = "/";

    public void Show(string redirectUrl = "/")
    {
        _redirectUrl = redirectUrl;
        _modalDisplay = "block;";
        _modalClass = "show";
        _showBackdrop = true;
        StateHasChanged();
    }

    private void Hide()
    {
        _modalDisplay = "none;";
        _modalClass = "";
        _showBackdrop = false;
        StateHasChanged();
        Navigation.NavigateTo(_redirectUrl);
    }
}
