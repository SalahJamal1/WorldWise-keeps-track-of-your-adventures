using MappyApplication.Exceptions;

namespace MappyApplication.MiddleWare;

public class ExceptionMiddleWare
{
    private readonly ILogger<ExceptionMiddleWare> _logger;

    private readonly RequestDelegate _next;

    public ExceptionMiddleWare(ILogger<ExceptionMiddleWare> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        try
        {
            await _next(ctx);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured");
            await HandelAsync(ctx, e);
        }
    }


    private async Task HandelAsync(HttpContext ctx, Exception ex)
    {
        var statusCode = ex switch
        {
            AppErrorResponse => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        await AppErrorResponse.HandelException(ctx, statusCode, ex.Message);
    }
}