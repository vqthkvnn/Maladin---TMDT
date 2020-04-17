using Maladin.Areas.Partner.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Maladin.Areas.Partner.Controllers
{
    public class BaseController : Controller
    {
        // GET: Partner/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sess = Session[LoginPartnerSession.USER_SESSION];
            if (sess == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { action = "Index", controller = "Login", Area = "Partner" }));

            }

        }
        
    }
}