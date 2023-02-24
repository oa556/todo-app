namespace TodoApp.Shared.Models;

public record TodoItemDto(Guid Id,
                          string Title,
                          string Author,
                          string Url,
                          TodoItemStatus Status,
                          DateTime DateAdded,
                          DateTime? DateCompleted);
