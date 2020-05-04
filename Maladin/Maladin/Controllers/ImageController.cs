using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maladin.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index(string path)
        {
            var dir = Server.MapPath("/");
            var pathImage = Path.Combine(dir, path); //validate the path for security or use other means to generate the path.
            return base.File(pathImage, "image/jpeg");
        }
        
    }
}