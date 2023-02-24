using System.Net;
using System.Text.Json;
using System.Web;
using Mapster;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using TodoApp.Api.Application.Commands.CreateTodoItem;
using TodoApp.Api.Domain;
using TodoApp.Shared.Models;
using TodoApp.Shared.Requests;

namespace TodoApp.Api.Presentation;

public class CreateTodoItem
{
    private readonly IMediator _mediator;

    public CreateTodoItem(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Function("CreateTodoItem")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "todo")]
        HttpRequestData req)
    {
        string body = await new StreamReader(req.Body).ReadToEndAsync();
        CreateTodoItemRequest? request = JsonSerializer.Deserialize<CreateTodoItemRequest>(body);
        if (request == null)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        try
        {
            TodoItemDto itemResponse = await _mediator.Send(request.Adapt<CreateTodoItemCommand>());
            HttpResponseData response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(itemResponse);
            return response;
        }
        catch (EntityNotFoundException)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
