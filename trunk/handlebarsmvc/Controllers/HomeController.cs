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
            //获取数据模板
            dynamic datatemplate = HandlebarsFileSystem.GetDataContent("Index");
            //修改数据模板中的属性值
            datatemplate["title"] = "Handlebars.Net-Demo-ByCSharp";

            return View("Index", datatemplate);
        }
    }
}