using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.MidMiddleware
{
internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server error"
        };
        switch (exception)
        {
            case NotFoundOrderByIdException notFoundOrderByID:
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Title = "Order not found";
                problemDetails.Detail = notFoundOrderByID.Message;
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                break;
            case NotFoundOrdersException notFoundOrders:
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Title = "Order not found";
                problemDetails.Detail = notFoundOrders.Message;
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                break;
            case NotFoundClientsExeption notFoundClients:
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Title = "Client not found";
                problemDetails.Detail = notFoundClients.Message;
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                break;
            case NotFoundFleetsExeption notFoundFleets:
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Title = "Fleet not found";
                problemDetails.Detail = notFoundFleets.Message;
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                break;
            case NotFoundPrductsExeption notFoundPrducts:
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Title = "Prducts not found";
                problemDetails.Detail = notFoundPrducts.Message;
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                break;
            case NotFoundRoutesExeption notFoundRoutes:
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Title = "Routes not found";
                problemDetails.Detail = notFoundRoutes.Message;
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                break;
            default:
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "Server error";
                problemDetails.Detail = "An unexpected error occurred as a result.";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
}