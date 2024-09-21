using EcommerceApi.V1.Commons.Exceptions;
using EcommerceApi.V1.Commons.ResponseModels;
using System.Threading.Tasks;
namespace EcommerceApi.V1.Commons.MIddlewares;

public class EcommerceExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<EcommerceExceptionMiddleware> _logger;

    public EcommerceExceptionMiddleware(RequestDelegate next, ILogger<EcommerceExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (EcommerceException ex)
        {
            await HandleException(context, ex.Code, ex.Message, ex.Global);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            await HandleException(context, 500, "", true);
        }
    }

    public async Task HandleException(HttpContext context, int code, string message, bool? Global)
    {
        context.Response.StatusCode = code;
        await context.Response.WriteAsJsonAsync(
            new ResponseModel<string>
            {
                Status = false,
                Error = message,
                Data = null,
                GlobalError = Global
            }
        );
    }
}
