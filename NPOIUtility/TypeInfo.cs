using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;

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
        /// 数据起始行
        /// </summary>
        private int m_dataStartRowNumber = 0;

        /// <summary>
        /// 使用的表对象
        /// </summary>
        private ISheet m_useSheet = null;

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

        /// <summary>
        /// 数据准备
        /// </summary>
        /// <param name="inputWorkbook"></param>
        private void PrepareData(IWorkbook inputWorkbook)
        {
            m_useSheet = null;
            m_dataStartRowNumber = 0;

            //利用索引
            if (null != m_useClassAttribute.SheetIndex)
            {
                m_useSheet = inputWorkbook.GetSheetAt(m_useClassAttribute.SheetIndex.Value);
            }
            else
            {
                m_useSheet = inputWorkbook.GetSheet(m_useClassAttribute.SheetName);
            }

        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal List<object> ReadWorkBook(IWorkbook input)
        {
            PrepareData(input);

            return null;
        }



    }
}
