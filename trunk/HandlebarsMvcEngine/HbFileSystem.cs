using HandlebarsDotNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HandlebarsMvcEngine
{
    /// <summary>
    /// Handlebars模板文件系统，该类用于读取和存储模板内容
    /// </summary>
    public class HbFileSystem : ViewEngineFileSystem, IEnumerable
    {
        // 视图文件的物理路径
        private string _viewPhysicalPath;

        public HbFileSystem(string viewPhysicalPath)
        {
            _viewPhysicalPath = viewPhysicalPath;
        }

        /// <summary>
        /// 模板字典，文件模板的文件路径为key，模板内容为content
        /// </summary>
        SortedDictionary<string, string> templateDictionary = new SortedDictionary<string, string>();

        /// <summary>
        /// 添加文件模板到字典的方法
        /// </summary>
        /// <param name="filePath">文件模板的文件路径，使用绝对路径，如：/HbViews/Home/Index.hbhtml</param>
        /// <param name="fileContent">文件模板内容</param>
        public void Add(string filePath, string fileContent)
        {
            templateDictionary[filePath] = fileContent;
        }

        /// <summary>
        /// 获取模板文件内容
        /// </summary>
        /// <param name="filePath">文件模板的文件路径，使用绝对路径，如：/HbViews/Home/Index.hbhtml</param>
        /// <returns>模板文件内容</returns>
        public override string GetFileContent(string filePath)
        {
            if (!FileExists(filePath))
            {
                Add(filePath, File.ReadAllText(CombinePath(_viewPhysicalPath, Sanitise(filePath))));
            }

            return templateDictionary[filePath];
        }

        /// <summary>
        /// 替换文件路由中的斜杠为反斜杠
        /// </summary>
        /// <param name="filePath">文件模板的文件路径，使用绝对路径，如：/HbViews/Home/Index.hbhtml</param>
        /// <returns>返回带反斜杠的文件路径</returns>
        private static string Sanitise(string filePath)
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

        /// <summary>
        /// 拼接文件完整路径
        /// </summary>
        /// <param name="dir">根目录</param>
        /// <param name="filePath">文件模板的文件路径，使用绝对路径，如：/HbViews/Home/Index.hbhtml</param>
        /// <returns>返回文件完整路径</returns>
        protected override string CombinePath(string dir, string filePath)
        {
            string path = Path.Combine(dir, Sanitise(filePath).TrimStart('\\'));
            return path;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
