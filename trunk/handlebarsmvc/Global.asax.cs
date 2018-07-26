using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HandlebarsMvcEngine;

namespace handlebarsmvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //定义HandlebarsMVC文件系统全局变量
        public static HbFileSystem HandlebarsFileSystem = new HbFileSystem(System.AppDomain.CurrentDomain.BaseDirectory);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            // ---------------------------------------------- 分隔线 ------------------------------------------------------- //
            

            //清空所有视图引擎
            ViewEngines.Engines.Clear();

            //添加自定义静态目录名，要求目录放在项目的根目录下
            //HandlebarsFileSystem.AddStaticPath("wwwtpl2");

            //注册自定义视图引擎
            ViewEngines.Engines.Add(new HbViewEngine(HandlebarsFileSystem));
        }
    }
}
