using HandlebarsDotNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;

namespace HandlebarsMvcEngine
{
    /// <summary>
    /// Handlebars模板文件系统，该类用于读取和存储模板内容
    /// </summary>
    public class HbFileSystem : ViewEngineFileSystem, IEnumerable
    {
        /// <summary>
        /// 固定存在的静态目录名
        /// </summary>
        const string defaultStaticPath = "wwwtpl";

        /// <summary>
        /// 模板字典，文件模板的文件路径为key，模板内容为content
        /// </summary>
        SortedDictionary<string, HbTemplate> templateDictionary = new SortedDictionary<string, HbTemplate>();

        /// <summary>
        /// 静态目录名和相应的子目录名集合
        /// </summary>
        public SortedDictionary<string, List<string>> StaticPath { get; set; }




        // 视图文件的物理路径
        private string _viewPhysicalPath;
        public HbFileSystem(string viewPhysicalPath)
        {
            _viewPhysicalPath = viewPhysicalPath;
            StaticPath = new SortedDictionary<string, List<string>>
            {
                { defaultStaticPath, findChildrenPathInStaticPath(defaultStaticPath) }
            };
        }




        #region [静态目录名相关方法]
        /// <summary>
        /// 添加单个静态目录名
        /// </summary>
        /// <param name="pathname">静态目录名</param>
        public void AddStaticPath(string pathname)
        {
            if (!StaticPath.ContainsKey(pathname))
                StaticPath.Add(pathname, findChildrenPathInStaticPath(pathname));
        }
        /// <summary>
        /// 添加静态目录名集合
        /// </summary>
        /// <param name="pathnames">静态目录名集合</param>
        public void AddStaticPathRange(List<string> pathnames)
        {
            foreach (string pn in pathnames)
            {
                AddStaticPath(pn);
            }
        }

        private List<string> findChildrenPathInStaticPath(string staticpath)
        {
            List<string> childrenPaths = new List<string>();
            string root = CombinePath(_viewPhysicalPath, staticpath);
            if (Directory.Exists(root))
            {
                DirectoryInfo diRoot = new DirectoryInfo(root);
                DirectoryInfo[] diChildren = diRoot.GetDirectories();
                if (diChildren != null && diChildren.Count() > 0)
                {
                    foreach (DirectoryInfo dic in diChildren)
                    {
                        childrenPaths.Add(dic.FullName.Replace(root, "").Replace(@"\", ""));
                    }
                }
            }

            return childrenPaths;
        }

        #endregion


        #region [模板字典相关方法]
        /// <summary>
        /// 添加文件模板到字典的方法
        /// </summary>
        /// <param name="filePath">文件模板的文件路径，使用绝对路径，如：/wwwtpl/Index.hbhtml</param>
        /// <param name="fileContent">文件模板内容</param>
        public void Add(string filePath, string fileContent, dynamic dataContent)
        {
            templateDictionary[filePath] = new HbTemplate(fileContent, dataContent);
        }

        /// <summary>
        /// 获取模板文件内容
        /// </summary>
        /// <param name="filePath">文件模板的文件路径，使用绝对路径，如：/wwwtpl/Index.hbhtml</param>
        /// <returns>模板文件内容</returns>
        public override string GetFileContent(string filePath)
        {
            HbTemplate hbt = GetHbTemplateObj(filePath);

            return hbt.ViewTemplate;
        }

        /// <summary>
        /// 获取数据模板内容
        /// </summary>
        /// <param name="filePath">文件模板的文件路径，使用绝对路径，如：/wwwtpl/Index.hbhtml（或视图文件名，不包含扩展名）</param>
        /// <returns></returns>
        public dynamic GetDataContent(string filePath)
        {
            if (!Regex.IsMatch(filePath, @"\/", RegexOptions.IgnoreCase))
                filePath = string.Format("/wwwtpl/{0}.hbhtml", filePath.Replace(".hbhtml", ""));

            HbTemplate hbt = GetHbTemplateObj(filePath);

            return hbt.DataTemplate;
        }

        /// <summary>
        /// 获取Handlebars模板对象
        /// </summary>
        /// <param name="filePath">文件模板的文件路径，使用绝对路径，如：/wwwtpl/Index.hbhtml</param>
        /// <returns></returns>
        private HbTemplate GetHbTemplateObj(string filePath)
        {
            if (!FileExists(filePath))
            {
                //读取页面模板和对应的数据模板
                string tpl = File.ReadAllText(CombinePath(_viewPhysicalPath, sanitise(filePath)));
                string datatpl = File.ReadAllText(CombinePath(_viewPhysicalPath, coverDataPath(filePath)));

                ////自定义DynamicJsonConverter方法，转换datatpl为dynamic类型
                //var serializer = new JavaScriptSerializer();
                //serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
                //Add(filePath, tpl, serializer.Deserialize<object>(datatpl));

                //原生方法，转换datatpl为dynamic类型
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Add(filePath, tpl, jss.Deserialize<dynamic>(datatpl));
            }

            return templateDictionary[filePath];
        }

        /// <summary>
        /// 将文件模板的文件路径转换为数据模板路径
        /// </summary>
        /// <param name="filePath">文件模板的文件路径，使用绝对路径，如：/wwwtpl/Index.hbhtml</param>
        /// <returns></returns>
        private static string coverDataPath(string filePath)
        {
            string datapath = "";

            if (Regex.IsMatch(filePath, @"[^\/]+(?=\.hbhtml)", RegexOptions.IgnoreCase))
            {
                datapath = "/wwwtpl/js/json/" + Regex.Match(filePath, @"[^\/]+(?=\.hbhtml)", RegexOptions.IgnoreCase).Value + ".json";
            }

            return sanitise(datapath);
        }

        /// <summary>
        /// 替换文件路由中的斜杠为反斜杠
        /// </summary>
        /// <param name="filePath">文件模板的文件路径，使用绝对路径，如：/HbViews/Home/Index.hbhtml</param>
        /// <returns>返回带反斜杠的文件路径</returns>
        private static string sanitise(string filePath)
        {
            return filePath.Replace("~", "").Replace("/", @"\");
        }

        /// <summary>
        /// 判断模板字典
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public override bool FileExists(string filePath)
        {
            return templateDictionary.ContainsKey(filePath);
        }
        #endregion


        /// <summary>
        /// 拼接文件完整路径
        /// </summary>
        /// <param name="dir">根目录</param>
        /// <param name="filePath">文件模板的文件路径，使用绝对路径，如：/HbViews/Home/Index.hbhtml</param>
        /// <returns>返回文件完整路径</returns>
        protected override string CombinePath(string dir, string filePath)
        {
            string path = Path.Combine(dir, sanitise(filePath).TrimStart('\\'));
            return path;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
