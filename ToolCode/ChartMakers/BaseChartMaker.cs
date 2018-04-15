using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolCode
{
    /// <summary>
    /// 图表制作器基类
    /// </summary>
    public abstract class BaseChartMaker
    {
        /// <summary>
        /// 使用的宽度
        /// </summary>
        protected int m_useWidth = 500;

        /// <summary>
        /// 使用的高度
        /// </summary>
        protected int m_useHight = 500;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="inputWidth"></param>
        /// <param name="inputHight"></param>
        public BaseChartMaker(int inputWidth = 500,int inputHight = 500)
        {
            m_useWidth = inputWidth;
            m_useHight = inputHight;
        }

    }
}
