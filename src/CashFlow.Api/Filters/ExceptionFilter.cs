using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
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
            ThrowUnknowException(context);
        }
    }

    public void HandleProjectException(ExceptionContext context)
    {
        var cashFlowException = (CashFlowException)context.Exception;
        var errorResponse = new ResponseErrorsJson(cashFlowException.GetErrors());
        
        context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    public void ThrowUnknowException(ExceptionContext context)
    {
        var errorMessages = new ResponseErrorsJson(ResourceErrorMessages.UNKNOW_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorMessages);
    }
}
