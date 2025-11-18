namespace MappyApplication.Exceptions;

public class AppErrorResponse : ApplicationException
{
    public AppErrorResponse(string message) : base(message)
    {
    }

    public static async Task HandelException(HttpContext ctx, int statusCode, string message)
    {
        ctx.Response.ContentType = "application/json";
        ctx.Response.StatusCode = statusCode;
        var error = new ErrorDetails
        {
            message = message,
            status = "failure"
        };
        await ctx.Response.WriteAsJsonAsync(error);
    }
}