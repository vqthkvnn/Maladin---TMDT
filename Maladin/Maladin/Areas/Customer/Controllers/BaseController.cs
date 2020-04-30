using Maladin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Maladin.Areas.Customer.Common;

namespace Maladin.Areas.Customer.Controllers
{
    public class BaseController : Controller
    {
        // GET: Customer/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sess = Session[CustomerSession.CUSTOMER_SESSION];
            if (sess == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { action = "Index", controller = "Login", Area= "Customer" }));

            }

        }
    }
}