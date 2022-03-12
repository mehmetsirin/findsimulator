using FindSimulator.Infrastructure.EventBus.Event;
using FindSimulator.Share.Event;
using FindSimulator.Share.RabbitMq;

using Microsoft.AspNetCore.Mvc.Filters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static Fleet.Share.ComplexTypes.CommonEnum;

namespace FindSimulator.Api.Action
{
    public class LogEventAction : ActionFilterAttribute
    {
        private readonly IEventBus _eventBus;

        public LogEventAction(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var userID = 1;
            var paramsGet = actionContext.ActionArguments;
            string paramsData = "[";
            var url = actionContext.HttpContext.Request.Path.Value;
            if (actionContext.HttpContext.Request.Method == "Get")
            {
                if (paramsGet.Count > 0)
                {
                    foreach (var item in paramsGet)
                    {
                        url = url + "?";
                        url = url + item.Key + "=" + item.Value;
                    }

                }

            }
            else
            {
                foreach (var item in paramsGet)
                {
                    paramsData = paramsData + "{" + item.Key + ":" + item.Value + "},";
                }
                paramsData = paramsData + "]";
            }


            if (url != null)
                _eventBus.Publish(new LogEvent() { IP = actionContext.HttpContext.Connection.RemoteIpAddress.ToString(), Action = url, Content = paramsData, UserID = userID });


            base.OnActionExecuting(actionContext);
        }
    }
}
