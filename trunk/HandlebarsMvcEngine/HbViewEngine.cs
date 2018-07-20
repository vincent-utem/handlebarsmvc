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
        // public HbViewEngine(HbFileSystem hdbfs) : this(hdbfs, new string[] { "~/wwwtpl/views/{1}/{0}.hbhtml", "~/wwwtpl/views/Shared/{0}.hbhtml" })
        //{

        //}

        public HbViewEngine(HbFileSystem hdbfs)
        {
            this.HdbFileSystem = hdbfs;
            base.ViewLocationFormats = getViewLocations();
        }

        //返回所有视图所在的目录，约定：视图存放在静态目录下
        private string[] getViewLocations()
        {
            string[] vls = new string[HdbFileSystem.StaticPath.Count];
            int i = 0;
            foreach (var sp in HdbFileSystem.StaticPath)
            {
                vls[i] = "~/" + sp.Key + "/{0}.hbhtml";
                i++;
            }
            return vls;
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
