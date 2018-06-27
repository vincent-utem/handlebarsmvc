﻿using handlebarsmvc.Models.HbTemplateEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace handlebarsmvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //注册自定义视图
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new HbViewEngine());
        }
    }
}
