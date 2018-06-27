using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace handlebarsmvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var data = new
            {
                title = "Handlebars.Net-Demo",
                content = "hello Handlebars.Net !!!"
            };

            return View(data);
        }
    }
}