using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace EnigmaShared.Filter
{
	public class CustomExceptionFilter : IActionFilter, IOrderedFilter
	{
        public int Order => int.MaxValue - 10;
        private readonly ILogger _log;
		public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
		{
            _log = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //if (context.Exception is BusinessException exception)
            //{
            //    context.Exception = new ObjectResult(new { Mensagem = context. })
            //    {
            //        StatusCode = exception.Status,
            //    };
            //    context.ExceptionHandled = true;
            //}
            //else if (context.Exception is Exception)
            //{
            //    var message = context.Exception.InnerException != null ? context.Exception.InnerException.Message : context.Exception.Message;

            //    context.Result = new ObjectResult(new RetornoExceptionApi(message))
            //    {
            //        StatusCode = StatusCodes.Status500InternalServerError
            //    };

            //    context.ExceptionHandled = true;
            //}
        }
    }
}