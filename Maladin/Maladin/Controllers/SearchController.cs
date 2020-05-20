using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.DAO;
using Maladin.EF;

namespace Maladin.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string q, string type, string page)
        {
            int p;
            if (page ==null)
            {
                p = 1;
            }
            else
            {
                p = Convert.ToInt32(page);
            }
            var dao = new ProductHomeDAO();
            Stopwatch sw = Stopwatch.StartNew();
            var res = dao.searchBy(q, type, p);
            ViewBag.q = q;
            ViewBag.count = res.Count();
            sw.Stop();
            ViewBag.time = sw.Elapsed.TotalMilliseconds;
            return View(res);
        }
    }
}