using HandlebarsDotNet;
using HtmlAgilityPack;
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


            // ---------------------------------------------- 分隔线 ------------------------------------------------------- //


            //var template = Handlebars.Compile(_viewTemplateContent);
            //string html = template(viewContext.ViewData.Model);

            //// 呈现出解析后的内容
            //writer.Write(html);


            // ---------------------------------------------- 分隔线 ------------------------------------------------------- //


            //1、加载视图模板HTML文档
            HtmlDocument htmldoc = new HtmlDocument();
            htmldoc.LoadHtml(_viewTemplateContent);

            //2、取出模板中所有<script apply="hbtpl" type="text/x-handlebars-template">节点
            List<HtmlNode> scriptNodes = htmldoc.DocumentNode.SelectNodes("//script[@apply='hbtpl']").ToList();
            //将这些节点绑定数据，并替换视图模板HTML文档中对应的模板节点
            foreach (HtmlNode sn in scriptNodes)
            {
                var tpl = Handlebars.Compile(sn.InnerHtml);
                sn.ParentNode.ReplaceChild(HtmlNode.CreateNode(tpl(viewContext.ViewData.Model)), sn);
            }

            //3、取出模板中所有<script apply="hbjs" type="text/javascript">节点
            scriptNodes = htmldoc.DocumentNode.SelectNodes("//script[@apply='hbjs']").ToList();
            //删除这些节点
            foreach (HtmlNode sn in scriptNodes)
            {
                sn.Remove();
            }            

            //4、输出绑定数据后的视图文档
            writer.Write(htmldoc.DocumentNode.OuterHtml);

            
        }
    }
}