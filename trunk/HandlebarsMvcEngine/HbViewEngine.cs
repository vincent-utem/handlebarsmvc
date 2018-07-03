using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HandlebarsDotNet;

namespace HandlebarsMvcEngine
{
    /// <summary>
    /// Handlebars模板引擎，重写控制器层到view层之间的逻辑
    /// </summary>
    public class HbViewEngine : VirtualPathProviderViewEngine
    {
        public HbFileSystem HdbFileSystem { get; set; }

        public HbViewEngine(HbFileSystem hdbfs, string[] viewLocations)
        {
            this.HdbFileSystem = hdbfs;
            base.ViewLocationFormats = viewLocations;
        }

        // 自定义 View 路径格式
        public HbViewEngine(HbFileSystem hdbfs) : this(hdbfs, new string[] { "~/HbViews/{1}/{0}.hbhtml", "~/HbViews/Shared/{0}.hbhtml" })
        {

        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            throw new NotImplementedException();
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new HbView(this.HdbFileSystem.GetFileContent(viewPath));
        }
    }
}
