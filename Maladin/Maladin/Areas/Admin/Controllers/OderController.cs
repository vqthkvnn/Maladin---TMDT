﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maladin.Areas.Admin.Controllers
{
    public class OderController : Controller
    {
        // GET: Admin/Oder
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CancelOder()
        {
            return View();
        }
    }
}