using Microsoft.Azure.Functions.Worker.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Web;
using Grpc.Core;

namespace TodoApp.Api.Utilities;

public static class HttpUtility
{
    public static T? ParseQueryString<T>(this HttpRequestData request)
    {
        var nameValueCollection = System.Web.HttpUtility.ParseQueryString(request.Url.Query);
        var dict = nameValueCollection.Cast<string>().ToDictionary(k => k, v => nameValueCollection[v]);
        var json = JsonSerializer.Serialize(dict);
        T? result = JsonSerializer.Deserialize<T>(json,
            new JsonSerializerOptions { NumberHandling = JsonNumberHandling.AllowReadingFromString });
        return result;
    }
}
