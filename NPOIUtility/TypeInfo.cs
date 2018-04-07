using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        /// 使用的属性封装
        /// </summary>
        private List<TypePropertyInfo> m_lstPropertyInfos = new List<TypePropertyInfo>();

        /// <summary>
        /// 使用的表对象
        /// </summary>
        private ISheet m_useSheet = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="useType"></param>
        /// <param name="useClassAttribute"></param>
        internal TypeInfo(Type useType, ClassAttribute useClassAttribute,Dictionary<PropertyInfo,PropertyAttribute> inputPropertyMap)
        {
            m_thisType = useType;
            m_useClassAttribute = useClassAttribute;
            //制备成员
            foreach (var oneKVP in inputPropertyMap)
            {
                m_lstPropertyInfos.Add(new TypePropertyInfo(oneKVP.Key, oneKVP.Value));
            }

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
            if (0 <= m_useClassAttribute.SheetIndex)
            {
                m_useSheet = inputWorkbook.GetSheetAt(m_useClassAttribute.SheetIndex);
            }
            else
            {
                m_useSheet = inputWorkbook.GetSheet(m_useClassAttribute.SheetName);
            }

            int useDataRowIndex = 0;

            int tempDataRowIndex = 0;

            //初始化属性封装
            foreach (var onePropertyInfo in m_lstPropertyInfos)
            {
                onePropertyInfo.PrepareData(m_useSheet, this.m_useClassAttribute, out tempDataRowIndex);
                //使用行冒泡
                useDataRowIndex = Math.Max(useDataRowIndex, tempDataRowIndex);
            }

            //设置使用数据起始行号
            m_dataStartRowNumber = this.m_useClassAttribute.RealUseDataStartRowIndex < 0 ? useDataRowIndex : this.m_useClassAttribute.RealUseDataStartRowIndex;

        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal List<object> ReadWorkBook(IWorkbook input)
        {
            //准备数据
            PrepareData(input);

            int useLength = m_lstPropertyInfos.Count;

            string[] useValues = new string[useLength];

            List<object> returnValues = new List<object>();


            //逐行读取
            for (int useRowIndex = m_dataStartRowNumber; useRowIndex <= m_useSheet.LastRowNum; useRowIndex++)
            {
                var useRow = m_useSheet.GetRow(useRowIndex);

                //全Null行
                if (null == useRow)
                {
                    continue;
                }

                //读取列
                for (int propertyIndex = 0; propertyIndex < useLength; propertyIndex++)
                {
                    useValues[propertyIndex] = null;

                    var useCell = useRow.GetCell(m_lstPropertyInfos[propertyIndex].UseColumnIndex);

                    if (null == useCell)
                    {
                        continue;
                    }

                    useValues[propertyIndex] = useCell.ToString();
                }

                try
                {
                    //创建对象
                    var tempObject = Activator.CreateInstance(m_thisType);

                    //属性设值
                    for (int propertyIndex = 0; propertyIndex < useLength; propertyIndex++)
                    {
                        if (null != useValues[propertyIndex])
                        {
                            m_lstPropertyInfos[propertyIndex].SetValue(tempObject, useValues[propertyIndex]);
                        }
                    }

                    //添加
                    returnValues.Add(tempObject); 
                }
                //异常跳过
                catch (Exception)
                {
                    continue;
                }

            }

            return returnValues;
        }



    }
}
