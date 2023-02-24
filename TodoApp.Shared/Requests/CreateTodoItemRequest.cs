using System.ComponentModel.DataAnnotations;

namespace TodoApp.Shared.Requests;

public class CreateTodoItemRequest
{
    private const int MinTitleLength = 8;
    private const int MaxTitleLength = 1024;
    private const int MinAuthorLength = 4;
    private const int MaxAuthorLength = 512;
    private const int MinUrlLength = 4;
    private const int MaxUrlLength = 2048;

    [Required(ErrorMessage = "Title is required")]
    [MinLength(MinTitleLength, ErrorMessage = "Title should be at least 8 characters long")]
    [MaxLength(MaxTitleLength, ErrorMessage = "Title should be maximum 1024 characters long")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Author is required")]
    [MinLength(MinAuthorLength, ErrorMessage = "Author should be at least 4 characters long")]
    [MaxLength(MaxAuthorLength, ErrorMessage = "Author should be maximum 512 characters long")]
    public string Author { get; set; } = null!;

    [Required(ErrorMessage = "Url is required")]
    [MinLength(MinUrlLength, ErrorMessage = "Url should be at least 4 characters long")]
    [MaxLength(MaxUrlLength, ErrorMessage = "Url should be maximum 2048 characters long")]
    public string Url { get; set; } = null!;
}
