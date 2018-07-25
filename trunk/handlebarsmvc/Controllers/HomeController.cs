using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace handlebarsmvc.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            dynamic data = HandlebarsFileSystem.GetDataContent("Index");
            data["title"] = "Handlebars.Net-Demo-ByCSharp";

            return View("Index", data);
        }
    }
}