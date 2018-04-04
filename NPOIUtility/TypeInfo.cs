using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPOIUtility
{
    /// <summary>
    /// 使用的类型信息封装
    /// </summary>
    internal class TypeInfo
    {
        /// <summary>
        /// 使用的Type
        /// </summary>
        private Type m_thisType;

        /// <summary>
        /// 使用的类特性
        /// </summary>
        private ClassAttribute m_useClassAttribute;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="useType"></param>
        /// <param name="useClassAttribute"></param>
        internal TypeInfo(Type useType, ClassAttribute useClassAttribute)
        {
            m_thisType = useType;
            m_useClassAttribute = useClassAttribute;
        }



    }
}
