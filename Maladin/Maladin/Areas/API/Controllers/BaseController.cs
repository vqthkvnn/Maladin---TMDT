using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Maladin.Areas.API.Common;


namespace Maladin.Areas.API.Controllers
{
    public class BaseController : Controller
    {
        // GET: API/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sess = Session[LoginSession.USER_SESSION];
            if (sess == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { action = "Index", controller = "Login", Area = "API"}));

            }

        }
    }
}