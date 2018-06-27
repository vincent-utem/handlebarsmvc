using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace handlebarsmvc.Models.HbTemplateEngine
{
    public class HbViewEngine : VirtualPathProviderViewEngine
    {
        public HbViewEngine()
        {
            // 自定义 View 路径格式
            base.ViewLocationFormats = new string[]
            {
                "~/HbViews/{1}/{0}.hbhtml", "~/HbViews/Shared/{0}.hbhtml"
            };
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            //return base.CreateView(controllerContext, viewPath, masterPath);

            var physicalPath = controllerContext.HttpContext.Server.MapPath(viewPath);

            return new HbView(physicalPath);
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {



            return this.CreatePartialView(controllerContext, partialPath);
        }
    }
}