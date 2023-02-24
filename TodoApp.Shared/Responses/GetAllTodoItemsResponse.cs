using TodoApp.Shared.Models;

namespace TodoApp.Shared.Responses;

public record GetAllTodoItemsResponse(TodoItemDto[] Items,
                                      MetaData MetaData);
