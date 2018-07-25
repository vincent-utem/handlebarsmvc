using HandlebarsMvcEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace handlebarsmvc.Controllers
{
    public class BaseController : Controller
    {
        //指向到全局Handlebars引擎的文件系统对象
        protected HbFileSystem HandlebarsFileSystem = MvcApplication.HandlebarsFileSystem;
    }
}