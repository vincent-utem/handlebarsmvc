using HandlebarsDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HandlebarsMvcEngine
{
    /// <summary>
    /// Handlebars模板类，重写Handlebars模板的view层的展现逻辑
    /// </summary>
    public class HbView : IView
    {
        //// 视图文件的物理路径
        //private string _viewPhysicalPath;

        //public HbView(string viewPhysicalPath)
        //{
        //    _viewPhysicalPath = viewPhysicalPath;
        //}

        private string _viewTemplateContent;

        public HbView(string viewTemplateContent)
        {
            _viewTemplateContent = viewTemplateContent;
        }

        /// <summary>
        /// 实现 IView 接口的 Render() 方法
        /// </summary>
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            //// 获取视图文件的原始内容
            //string rawContents = File.ReadAllText(_viewPhysicalPath);
            //var template = Handlebars.Compile(rawContents);

            //// 根据自定义的规则解析原始内容  
            //string parsedContents = Parse(rawContents, viewContext.ViewData);

            //// 呈现出解析后的内容
            //writer.Write(parsedContents);

            //var template = Handlebars.CompileView(_viewPhysicalPath);


            //string html = template(viewContext.ViewData.Model);

            //// 呈现出解析后的内容
            //writer.Write(html);



            var template = Handlebars.Compile(_viewTemplateContent);
            string html = template(viewContext.ViewData.Model);
            // 呈现出解析后的内容
            writer.Write(html);
        }
    }
}