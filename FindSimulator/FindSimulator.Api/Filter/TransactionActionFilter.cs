using FindSimulator.Api.Controllers;
using FindSimulator.Service.Core;

using Microsoft.AspNetCore.Mvc.Filters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindSimulator.Api.Filter
{
    public class TransactionActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ((BaseController)context.Controller).BusinessManagerFactory._unitOfWork.BeginTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
                ((BaseController)context.Controller).BusinessManagerFactory._unitOfWork.RollbackTransaction();
            else
                ((BaseController)context.Controller).BusinessManagerFactory._unitOfWork.Complete();
        }
    }
}
