using TodoApp.Shared.Models;

namespace TodoApp.Shared.Requests;

public record UpdateTodoItemRequest(Guid Id,
                                    TodoItemStatus Status);
