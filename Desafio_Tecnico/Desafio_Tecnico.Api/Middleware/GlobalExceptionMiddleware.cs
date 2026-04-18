public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exceção não tratada");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new { mensagem = "", statusCode = 500 };

        if (exception is HttpRequestException httpEx)
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            response = new { mensagem = "Servidor indisponível. Tente novamente.", statusCode = 503 };
        }
        else if (exception is TimeoutException)
        {
            context.Response.StatusCode = StatusCodes.Status504GatewayTimeout;
            response = new { mensagem = "Operação expirou. Tente novamente.", statusCode = 504 };
        }
        else if (exception is TaskCanceledException)
        {
            context.Response.StatusCode = StatusCodes.Status504GatewayTimeout;
            response = new { mensagem = "Requisição cancelada.", statusCode = 504 };
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            response = new { mensagem = "Erro ao processar solicitação.", statusCode = 500 };
        }

        return context.Response.WriteAsJsonAsync(response);
    }
}
