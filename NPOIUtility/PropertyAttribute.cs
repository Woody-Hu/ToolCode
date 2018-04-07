using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPOIUtility
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    /// <summary>
    /// 属性特性
    /// </summary>
    public class PropertyAttribute : Attribute
    {
        /// <summary>
        /// 使用的列索引
        /// </summary>
        private int m_useColumnIndex = -1;

        /// <summary>
        /// 使用的列名称
        /// </summary>
        private string m_useColumnName = null;

        /// <summary>
        /// 使用的列索引
        /// </summary>
        public int UseColumnIndex
        {
            get
            {
                return m_useColumnIndex;
            }

            set
            {
                m_useColumnIndex = value;
            }
        }

        /// <summary>
        /// 使用的列名称
        /// </summary>
        public string UseColumnName
        {
            get
            {
                return m_useColumnName;
            }

            set
            {
                m_useColumnName = value;
            }
        }
    }
}
