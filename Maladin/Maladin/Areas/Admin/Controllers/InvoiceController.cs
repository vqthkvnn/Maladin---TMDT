using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maladin.Areas.Admin.Controllers
{
    public class InvoiceController : BaseController
    {
        // GET: Admin/Invoice
        public ActionResult Index()
        {
            return View();
        }
    }
}