using System.ComponentModel.DataAnnotations;
using TodoApp.Shared.Models;

namespace TodoApp.Api.Domain;

public class TodoItem
{
    public const int MinTitleLength = 8;
    public const int MaxTitleLength = 1024;
    public const int MinAuthorLength = 4;
    public const int MaxAuthorLength = 512;
    public const int MinUrlLength = 4;
    public const int MaxUrlLength = 2048;

    private TodoItem() { }

    public Guid Id { get; private init; }
    public string Title { get; private init; } = null!;
    public string Author { get; private init; } = null!;
    public string Url { get; private init; } = null!;
    public TodoItemStatus Status { get; private set; }
    public DateTime DateAdded { get; private init; }
    public DateTime? DateCompleted { get; private set; }

    public static TodoItem Create(string title,
                                  string author,
                                  string url)
    {
        if (string.IsNullOrEmpty(title) || title.Length is < MinTitleLength or > MaxTitleLength)
        {
            throw new ValidationException(nameof(title));
        }
        if (string.IsNullOrEmpty(author) || author.Length is < MinAuthorLength or > MaxAuthorLength)
        {
            throw new ValidationException(nameof(author));
        }
        if (string.IsNullOrEmpty(url) || url.Length is < MinUrlLength or > MaxUrlLength)
        {
            throw new ValidationException(nameof(url));
        }

        return new TodoItem
        {
            Id = Guid.NewGuid(),
            Title = title,
            Author = author,
            Url = url,
            Status = TodoItemStatus.Todo,
            DateAdded = DateTime.UtcNow
        };
    }

    public void Complete()
    {
        DateCompleted = DateTime.UtcNow;
        Status = TodoItemStatus.Completed;
    }

    public void Unсomplete()
    {
        DateCompleted = null;
        Status = TodoItemStatus.Todo;
    }
}
