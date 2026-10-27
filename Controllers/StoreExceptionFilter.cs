using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using para.Models;

namespace para.Controllers;

public class StoreExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not StoreException exception) return;
        var status = exception.Error switch
        {
            StoreError.NotFound => StatusCodes.Status404NotFound,
            StoreError.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };
        context.Result = new ObjectResult(new ProblemDetails
        {
            Status = status,
            Title = exception.Message
        }) { StatusCode = status };
        context.ExceptionHandled = true;
    }
}
