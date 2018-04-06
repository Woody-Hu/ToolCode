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
    /// 类型属性封装
    /// </summary>
    internal class TypePropertyInfo
    {
        /// <summary>
        /// 使用的属性封装
        /// </summary>
        private PropertyInfo m_usePropertyInfo = null;

        /// <summary>
        /// 使用的属性特性
        /// </summary>
        private PropertyAttribute m_usePropertyAttribute = null;

        /// <summary>
        /// 使用的属性类型
        /// </summary>
        private Type m_useProperType;

        /// <summary>
        /// 粘贴方法对象
        /// </summary>
        private MethodInfo m_useProperTypeParseMethod;

        /// <summary>
        /// 使用的列索引
        /// </summary>
        private int m_useColumnIndex;

        /// <summary>
        /// 使用的字符串类型
        /// </summary>
        private static Type m_useStringType = typeof(string);

        /// <summary>
        /// 粘贴方法名
        /// </summary>
        private const string m_useParseMethodName = "Parse";
        
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="inputPropertyInfo"></param>
        /// <param name="inputPropertyAttribute"></param>
        internal TypePropertyInfo(PropertyInfo inputPropertyInfo,PropertyAttribute inputPropertyAttribute)
        {
            m_usePropertyInfo = inputPropertyInfo;
            m_usePropertyAttribute = inputPropertyAttribute;
            m_useProperType = m_usePropertyInfo.PropertyType;
        }

        /// <summary>
        /// 使用的列索引
        /// </summary>
        internal int UseColumnIndex
        {
            get { return m_useColumnIndex; }
            set { m_useColumnIndex = value; }
        }

        /// <summary>
        /// 检查属性是否可用
        /// </summary>
        /// <param name="inputPropertyInfo"></param>
        /// <returns></returns>
        internal static bool CheckProperty(PropertyInfo inputPropertyInfo)
        {
            //获取属性类型
            var propertyType = inputPropertyInfo.PropertyType;
            return m_useStringType != inputPropertyInfo && null == propertyType.GetMethod(m_useParseMethodName,BindingFlags.Static|BindingFlags.Public);
        }

        /// <summary>
        /// 准备数据
        /// </summary>
        /// <param name="inputSheet"></param>
        /// <param name="inputClassAttribute"></param>
        /// <param name="headerRowIndex"></param>
        internal void PrepareData(ISheet inputSheet,ClassAttribute inputClassAttribute,out int headerRowIndex)
        {
            headerRowIndex = 0;

            //已赋值列索引
            if (null != m_usePropertyAttribute.UseColumnIndex)
            {
                m_useColumnIndex = m_usePropertyAttribute.UseColumnIndex.Value;
                return;
            }

        }

        /// <summary>
        /// 设值
        /// </summary>
        /// <param name="inputObject"></param>
        /// <param name="inputValue"></param>
        internal void SetValue(object inputObject,string inputValue)
        {
            //若是字符串类型
            if (m_useProperType == m_useStringType)
            {
                m_usePropertyInfo.SetValue(inputObject, inputValue);
            }
            else
            {
                //设置粘贴方法引用
                if (null == m_useProperTypeParseMethod)
                {
                    m_useProperTypeParseMethod = m_useProperType.GetMethod(m_useParseMethodName, BindingFlags.Static | BindingFlags.Public);
                }
                //转换
                var realValue = m_useProperTypeParseMethod.Invoke(null, new object[] { inputValue });
                //设值
                m_usePropertyInfo.SetValue(inputObject, realValue);
            }
        }
    }
}
