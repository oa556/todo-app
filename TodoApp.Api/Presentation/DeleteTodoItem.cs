using System.Net;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using TodoApp.Api.Application.Commands.DeleteTodoItem;
using TodoApp.Api.Domain;

namespace TodoApp.Api.Presentation;

public class DeleteTodoItem
{
    private readonly IMediator _mediator;

    public DeleteTodoItem(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Function("DeleteTodoItem")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "todo/{id:Guid}")]
        HttpRequestData req,
        Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteTodoItemCommand(id));
            return req.CreateResponse(HttpStatusCode.OK);
        }
        catch (EntityNotFoundException)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
