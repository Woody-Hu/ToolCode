using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPOIUtility
{
    /// <summary>
    /// 图片插入请求封装
    /// </summary>
    public class PictureInsertRequest
    {
        /// <summary>
        /// 使用的图片
        /// </summary>
        public Image UseImage { set; get; }

        /// <summary>
        /// 起始行
        /// </summary>
        public int StartRow { set; get; }

        /// <summary>
        /// 起始列
        /// </summary>
        public int StartColumn { set; get; }

        /// <summary>
        /// 终止行
        /// </summary>
        public int EndRow { set; get; }

        /// <summary>
        /// 终止列
        /// </summary>
        public int EndColumn { set; get; }
    }
}
