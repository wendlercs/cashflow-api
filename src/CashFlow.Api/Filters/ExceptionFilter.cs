using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is CashFlowException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnkownError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        if(context.Exception is ErrorOnValidateException)
        {
            var ex = (ErrorOnValidateException)context.Exception;

            var errorMessage = new ResponseErrorJson(ex.Errors);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            context.Result = new BadRequestObjectResult(errorMessage);
        }
        else
        {
            var errorMessage = new ResponseErrorJson(context.Exception.Message);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            context.Result = new BadRequestObjectResult(errorMessage);
        }
    }

    private void ThrowUnkownError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson("Unknown Error");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        context.Result = new ObjectResult(errorResponse);
    }
}
