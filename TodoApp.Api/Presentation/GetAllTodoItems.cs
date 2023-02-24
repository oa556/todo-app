using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using TodoApp.Api.Application.Queries.GetAllTodoItems;
using TodoApp.Shared.Responses;
using MediatR;
using TodoApp.Shared.Requests;
using Mapster;
using TodoApp.Api.Utilities;

namespace TodoApp.Api.Presentation;

public class GetAllTodoItems
{
    private readonly IMediator _mediator;

    public GetAllTodoItems(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Function("GetAllTodoItems")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "todo")]
        HttpRequestData req)
    {
        GetAllTodoItemsRequest? request = req.ParseQueryString<GetAllTodoItemsRequest>();
        if (request == null)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        GetAllTodoItemsResponse itemsResponse = await _mediator.Send(request.Adapt<GetAllTodoItemsQuery>());
        HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(itemsResponse);
        return response;
    }
}
