using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using MvcApplication1.Services;
using System.Web.Routing;
using WebMatrix.WebData;

namespace MvcApplication1.Filters
{
    public class SubscriptionFilter : ActionFilterAttribute, IActionFilter
    {
        private PaymentService Repository;

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            Repository = new PaymentService();
            var controller = filterContext.Controller;

            if(!Repository.HasSub())
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Payment", action = "NoSub" })
                    );
                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
            }
            else
            {
                Repository.CheckSubscription(WebSecurity.CurrentUserId);

                if (!Repository.HasActiveSub())
                {
                    filterContext.Result = new RedirectToRouteResult(
                     new RouteValueDictionary(new { controller = "Payment", action = "SubscriptionExpired" })
                    );

                    filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                }
            }
        }
    }
}