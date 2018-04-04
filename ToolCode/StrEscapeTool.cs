using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToolCode
{
    class StrEscapeTool
    {
        #region 私有字段

        /// <summary>
        /// 转义的目标字符
        /// </summary>
        private const string m_strGoal = ";";

        /// <summary>
        /// 转义用字符
        /// </summary>
        private const string m_strEscape = "/";

        /// <summary>
        /// 补充用空白字符
        /// </summary>
        private const string m_strWihte = " ";

        /// <summary>
        /// 转义用正则表达式(线程安全)
        /// </summary>
        private static Regex m_UseEscapeRegex = new Regex(m_strEscape + "*" + m_strGoal);

        /// <summary>
        /// 反转义用的正则表达式(线程安全)
        /// </summary>
        private static Regex m_UseUnEsacpeRegex = new Regex(m_strEscape + "+" + m_strGoal);

        /// <summary>
        /// 解组用的正则表达式(线程安全)
        /// </summary>
        private static Regex m_UseUnMergeRegex = new Regex("(?<!" + m_strEscape + ")" + m_strGoal);
        #endregion

        /// <summary>
        /// 将一组字符串转义并增加分割字符合并
        /// </summary>
        /// <param name="input">输入的一组需转义合并的字符串</param>
        /// <returns>转义合并后的结果</returns>
        public static string EscapeAndMerge(List<string> input)
        {
            StringBuilder returnValue = new StringBuilder();

            //合并
            for (int tempIndex = 0; tempIndex < input.Count; tempIndex++)
            {
                //转义
                returnValue.Append(Escape(input[tempIndex]));
                //附加分割符
                if (tempIndex != input.Count - 1)
                {
                    //附加空白
                    returnValue.Append(m_strWihte + m_strGoal);
                }
            }

            return returnValue.ToString();
        }

        /// <summary>
        /// 将一个字符串解组并反转义
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>解组并反转义的结果</returns>
        public static List<string> UnEsapeAneUnMerge(string input)
        {
            List<string> returnValue = new List<string>();

            //字符串解组
            var tempValue = m_UseUnMergeRegex.Split(input);

            //反转义
            for (int tempIndex = 0; tempIndex < tempValue.Length; tempIndex++)
            {
                var oneValue = tempValue[tempIndex];
                if (tempIndex != tempValue.Length - 1)
                {
                    returnValue.Add(UnEscape(oneValue.Remove(oneValue.Length - 1, 1)));
                }
                else
                {
                    returnValue.Add(UnEscape(oneValue));
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 转义字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Escape(string input)
        {
            return EscapeMethod(input, true);
        }

        /// <summary>
        /// 反转义字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string UnEscape(string input)
        {
            return EscapeMethod(input, false);
        }

        /// <summary>
        /// 基础的转义方法
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="ifEscape">转义/非转义</param>
        /// <returns>改变后的字符串</returns>
        private static string EscapeMethod(string input, bool ifEscape)
        {
            StringBuilder returnValue = new StringBuilder();

            //创建正则表达式贪婪模式
            // 
            Regex useRegex = ifEscape ? m_UseEscapeRegex : m_UseUnEsacpeRegex;

            //上次的位置
            int lastIndex = 0;

            foreach (Match oneMathces in useRegex.Matches(input))
            {
                //附加上次结果
                returnValue.Append(input.Substring(lastIndex, oneMathces.Index - lastIndex));
                //附加转换结果
                returnValue.Append(ChangString(oneMathces.Value, ifEscape));
                //调整索引
                lastIndex = oneMathces.Index + oneMathces.Length;
            }

            //附加结尾数据
            if (lastIndex <= input.Length - 1)
            {
                returnValue.Append(input.Substring(lastIndex, input.Length - lastIndex));
            }

            return returnValue.ToString();
        }

        /// <summary>
        /// 转换字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="ifIsEscape"></param>
        /// <returns></returns>
        private static string ChangString(string input, bool ifIsEscape)
        {
            string returnValue = input;
            if (ifIsEscape)
            {
                returnValue = returnValue.Insert(0, m_strEscape);
            }
            else
            {
                returnValue = returnValue.Remove(0, 1);
            }

            return returnValue;
        }
    }
}
