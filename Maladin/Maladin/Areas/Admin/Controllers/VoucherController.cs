using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maladin.Areas.Admin.Controllers
{
    public class VoucherController : BaseController
    {
        // GET: Admin/Voucher
        public ActionResult Index()
        {
            return View();
        }
    }
}