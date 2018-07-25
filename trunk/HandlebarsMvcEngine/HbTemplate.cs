using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandlebarsMvcEngine
{
    /// <summary>
    /// HandlebarsMvc模板类
    /// </summary>
    public class HbTemplate
    {
        /// <summary>
        /// 视图模板内容
        /// </summary>
        public string ViewTemplate { get; set; }

        /// <summary>
        /// 数据模板内容
        /// </summary>
        public dynamic DataTemplate { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="view">视图模板内容</param>
        /// <param name="data">数据模板内容</param>
        public HbTemplate(string view, dynamic data)
        {
            this.ViewTemplate = view;
            this.DataTemplate = data;
        }
    }
}
