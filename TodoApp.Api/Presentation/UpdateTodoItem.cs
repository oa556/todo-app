using System.Net;
using System.Text.Json;
using Mapster;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using TodoApp.Api.Application.Commands.UpdateTodoItem;
using TodoApp.Api.Domain;
using TodoApp.Api.Utilities;
using TodoApp.Shared.Models;
using TodoApp.Shared.Requests;

namespace TodoApp.Api.Presentation;

public class UpdateTodoItem
{
    private readonly IMediator _mediator;

    public UpdateTodoItem(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Function("UpdateTodoItem")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "todo")]
        HttpRequestData req)
    {
        string body = await new StreamReader(req.Body).ReadToEndAsync();
        UpdateTodoItemRequest? request = JsonSerializer.Deserialize<UpdateTodoItemRequest>(body);
        if (request == null)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        try
        {
            TodoItemDto itemResponse = await _mediator.Send(request.Adapt<UpdateTodoItemCommand>());
            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(itemResponse);
            return response;
        }
        catch (EntityNotFoundException)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
