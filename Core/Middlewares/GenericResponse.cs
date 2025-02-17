using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public int StatusCode { get; set; }
}

public class ResponseMiddleware
{
    private readonly RequestDelegate _next;
    public ResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;
        try
        {

            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;


            await _next(context);


            memoryStream.Position = 0;
            var reader = new StreamReader(memoryStream);
            var responseBody = await reader.ReadToEndAsync();

            
            var response = new ApiResponse<object>
            {
                Success = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300,
                StatusCode = context.Response.StatusCode,
                Message = GetDefaultMessageForStatusCode(context.Response.StatusCode)
            };


            if (!string.IsNullOrEmpty(responseBody))
            {
                try
                {
                    response.Data = JsonSerializer.Deserialize<object>(responseBody)!;
                }
                catch (JsonException)
                {

                    response.Data = responseBody;
                }
                catch (Exception ex)
                {

                    response.Success = false;
                    response.StatusCode = 500;
                    response.Message = ex.Message;
                    context.Response.StatusCode = 500;
                }
            }


            context.Response.ContentType = "application/json";
            memoryStream.Position = 0;
            memoryStream.SetLength(0);

            await JsonSerializer.SerializeAsync(memoryStream, response);

            memoryStream.Position = 0;
            await memoryStream.CopyToAsync(originalBodyStream);
        }
        catch (Exception ex)
        {

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var errorResponse = new ApiResponse<object>
            {
                Success = false,
                StatusCode = 500,
                Message = ex.Message
            };

            await JsonSerializer.SerializeAsync(originalBodyStream, errorResponse);
        }
        finally
        {
            context.Response.Body = originalBodyStream;
        }
    }

    private string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            200 => "Request successful",
            201 => "Resource created successfully",
            204 => "No content",
            400 => "Bad request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Resource not found",
            500 => "Internal server error",
            _ => "Unknown status"
        };
    }
}


public static class ResponseMiddlewareExtensions
{
    public static IApplicationBuilder UseResponseWrapper(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ResponseMiddleware>();
    }
}
