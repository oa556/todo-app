using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace TodoApp.Api;

public class Test
{
    private readonly ILogger _logger;

    public Test(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<Test>();
    }

    [Function("Test")]
    public HttpResponseData Run([HttpTrigger(
        AuthorizationLevel.Anonymous,
        "get",
        Route = "test"
        )] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString("Welcome to Azure Functions!");

        return response;
    }
}
