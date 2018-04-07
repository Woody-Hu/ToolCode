﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

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
        /// 字典映射
        /// </summary>
        private static Dictionary<Type, TypeInfo> m_useTypeMap = new Dictionary<Type, TypeInfo>();

        /// <summary>
        /// 注册一个类
        /// </summary>
        /// <param name="inputType"></param>
        private void RegisteredType(Type inputType)
        {
            //若已存在
            if (m_useTypeMap.ContainsKey(inputType) || null == inputType)
            {
                return;
            }

            //获取类特性
            var classAttributes = inputType.GetCustomAttributes(m_useClassAttributeType, false);

            //获取检查
            if (null == classAttributes || 1 != classAttributes.Length)
            {
                m_useTypeMap.Add(inputType, null);
                return;
            }

            //获取使用的类特性
            var useClassAtrribute = (ClassAttribute)classAttributes[0];

            //判断特性是否可用
            if (0 > useClassAtrribute.SheetIndex && string.IsNullOrWhiteSpace(useClassAtrribute.SheetName))
            {
                m_useTypeMap.Add(inputType, null);
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
                    (0 > tempPropertyAttribute.UseColumnIndex
                    && string.IsNullOrWhiteSpace(tempPropertyAttribute.UseColumnName)))
                {
                    continue;
                }


                //若不是字符串类型且没有粘贴方法
                if (!TypePropertyInfo.CheckProperty(oneProperty))
                {
                    continue;
                }

                //添加到属性映射
                tempPropertyMap.Add(oneProperty, tempPropertyAttribute);
            }

            //注册
            if (0 != tempPropertyMap.Count)
            {
                m_useTypeMap.Add(inputType, new TypeInfo(inputType, useClassAtrribute, tempPropertyMap));
            }
            else
            {
                m_useTypeMap.Add(inputType, null);
            }
        }

        /// <summary>
        /// 尝试读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lstReadedValue"></param>
        /// <returns></returns>
        public bool TryRead<T> (string inputPath, out List<T> lstReadedValue)
            where T:class
        {
            lstReadedValue = new List<T>();

            Type useType = typeof(T);

            RegisteredType(useType);

            var useInfo = m_useTypeMap[useType];

            //若注册失败
            if (null == useInfo)
            {
                return false;
            }

            FileInfo useFieInfo = new FileInfo(inputPath);

            //若文件不存在
            if (!useFieInfo.Exists)
            {
                return false;
            }

            IWorkbook useWorkBook = null;

            //工厂制备WorkBook
            if (useFieInfo.Extension.ToLower().Equals(".xlsx"))
            {
                useWorkBook = new XSSFWorkbook(useFieInfo.FullName);
            }
            else if(useFieInfo.Extension.ToLower().Equals(".xls"))
            {
                using (FileStream fs = new FileStream(useFieInfo.FullName,FileMode.Open))
                {
                    useWorkBook = new HSSFWorkbook(fs);
                }
                
            }

            var returnValue = useInfo.ReadWorkBook(useWorkBook);

            lstReadedValue = returnValue.Cast<T>().ToList();

            return 0 != lstReadedValue.Count;

      
        }

    }
}
