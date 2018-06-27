using HandlebarsDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace handlebarsmvc.Models.HbTemplateEngine
{
    public class HbView : IView
    {
        // 视图文件的物理路径
        private string _viewPhysicalPath;

        public HbView(string viewPhysicalPath)
        {
            _viewPhysicalPath = viewPhysicalPath;
        }

        /// <summary>
        /// 实现 IView 接口的 Render() 方法
        /// </summary>
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            // 获取视图文件的原始内容
            string rawContents = File.ReadAllText(_viewPhysicalPath);

            //// 根据自定义的规则解析原始内容  
            //string parsedContents = Parse(rawContents, viewContext.ViewData);

            //// 呈现出解析后的内容
            //writer.Write(parsedContents);
            
            var template = Handlebars.Compile(rawContents);
            string html = template(viewContext.ViewData.Model);

            // 呈现出解析后的内容
            writer.Write(html);
        }
    }
}