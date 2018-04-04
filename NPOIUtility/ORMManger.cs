using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NPOIUtility
{
    /// <summary>
    /// 对象映射管理器
    /// </summary>
    public class ORMManger
    {
        /// <summary>
        /// 使用的类特性
        /// </summary>
        private static Type m_useClassAttributeType = typeof(ClassAttribute);

        /// <summary>
        /// 使用的属性特性
        /// </summary>
        private static Type m_usePropertyAttributeType = typeof(PropertyAttribute);

        /// <summary>
        /// 使用的字符串类型
        /// </summary>
        private static Type m_useStringType = typeof(string);

        /// <summary>
        /// 粘贴方法名
        /// </summary>
        private const string m_useParseMethodName = "Parse";

        /// <summary>
        /// 注册一个类
        /// </summary>
        /// <param name="inputType"></param>
        private void RegisteredType(Type inputType)
        {
            //获取类特性
            var classAttributes = inputType.GetCustomAttributes(m_useClassAttributeType, false);

            //获取检查
            if (null == classAttributes || 1 != classAttributes.Length)
            {
                return;
            }

            //获取使用的类特性
            var useClassAtrribute = (ClassAttribute)classAttributes[0];

            //判断特性是否可用
            if (null == useClassAtrribute.SheetIndex && string.IsNullOrWhiteSpace(useClassAtrribute.SheetName))
            {
                return;
            }

            //临时局部变量
            PropertyAttribute tempPropertyAttribute = null;

            //属性-属性特性映射字典
            Dictionary<PropertyInfo, PropertyAttribute> tempPropertyMap
                = new Dictionary<PropertyInfo, PropertyAttribute>();

            //获取公开属性
            foreach (var oneProperty in inputType.GetProperties())
            {
                //不可读可写 跳过
                if (!oneProperty.CanRead || !oneProperty.CanWrite)
                {
                    continue;
                }

                //获取临时特性
                tempPropertyAttribute = oneProperty.GetCustomAttribute(m_usePropertyAttributeType) as PropertyAttribute;
                //没有特性跳过
                //特性属性检查
                if (null == tempPropertyAttribute ||
                    (null == tempPropertyAttribute.UseColumnIndex
                    && string.IsNullOrWhiteSpace(tempPropertyAttribute.UseColumnName)))
                {
                    continue;
                }

                //获取属性类型
                var propertyType = oneProperty.PropertyType;

                //若不是字符串类型且没有粘贴方法
                if (m_useStringType != propertyType && null == propertyType.GetMethod(m_useParseMethodName))
                {
                    continue;
                }

                //添加到属性映射
                tempPropertyMap.Add(oneProperty, tempPropertyAttribute);
            }


        }


    }
}
