using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToolCode
{
    /// <summary>
    /// 简体中文字符公共方法
    /// </summary>
    public class SimpleChineseUtilityMethod
    {
        /// <summary>
        /// 全部是简体中文的匹配
        /// </summary>
        private const string m_strTotalSimpleChinesesPattern = @"^[\u4e00-\u9fa5]+$";

        /// <summary>
        /// 部分是简体中文的匹配
        /// </summary>
        private const string m_strPartSimpleChinesePattern = @"[\u4e00-\u9fa5]+";

        /// <summary>
        /// 全部是简体中文的匹配
        /// </summary>
        private static Regex m_totalSimpleChineseRegex = new Regex(m_strTotalSimpleChinesesPattern);

        /// <summary>
        /// 部分是简体中文的匹配
        /// </summary>
        private static Regex m_partSimpleChinesePatternRegex = new Regex(m_strPartSimpleChinesePattern);

        /// <summary>
        /// 判断输入字符串是否是简体中文
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="ifTotal">是否要求整体均是简体中文</param>
        /// <returns></returns>
        public static bool IfIsSimpleChinese(string input,bool ifTotal = false)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }


            Regex useRegex = ifTotal?m_totalSimpleChineseRegex:m_partSimpleChinesePatternRegex;

            return useRegex.IsMatch(input);
        }

        /// <summary>
        /// 获得输入字符串的中文部分的首字母列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<List<char>> GetFirstPYList(string input)
        {
            List<List<char>> returnValue = new List<List<char>>();

            if (string.IsNullOrWhiteSpace(input))
            {
                return returnValue;
            }
            else
            {
                var useMatchesValues = m_partSimpleChinesePatternRegex.Matches(input);

                foreach (Match oneMatch in useMatchesValues)
                {
                    List<char> tempLstChar = new List<char>();
                    returnValue.Add(tempLstChar);
                    foreach (var oneChar in oneMatch.Value)
                    {
                        var tempChar = GetSimpleChinesFirstPY(oneChar.ToString());

                        if (tempChar != null)
                        {
                            tempLstChar.Add(tempChar.Value);
                        }
                    }

                }

                return returnValue;
            }
        }

        /// <summary>
        /// 获取字符首字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static char? GetSimpleChinesFirstPY(string input)
        {
            try
            {
                long indexOfChar;

                byte[] tempCharBite = System.Text.Encoding.Default.GetBytes(input);

                //获取两个字节的相应值
                int i1 = (short)(tempCharBite[0]);
                int i2 = (short)(tempCharBite[1]);

                //获得字符索引
                indexOfChar = i1 * 256 + i2;
                
                //根据索引获取首字母
                if ((indexOfChar >= 45217) && (indexOfChar <= 45252))
                {

                    return 'A';

                }

                else if ((indexOfChar >= 45253) && (indexOfChar <= 45760))
                {

                    return 'B';

                }
                else if ((indexOfChar >= 45761) && (indexOfChar <= 46317))
                {

                    return 'C';

                }
                else if ((indexOfChar >= 46318) && (indexOfChar <= 46825))
                {

                    return 'D';

                }
                else if ((indexOfChar >= 46826) && (indexOfChar <= 47009))
                {

                    return 'E';

                }
                else if ((indexOfChar >= 47010) && (indexOfChar <= 47296))
                {

                    return 'F';

                }
                else if ((indexOfChar >= 47297) && (indexOfChar <= 47613))
                {

                    return 'G';

                }
                else if ((indexOfChar >= 47614) && (indexOfChar <= 48118))
                {

                    return 'H';

                }
                else if ((indexOfChar >= 48119) && (indexOfChar <= 49061))
                {

                    return 'J';

                }
                else if ((indexOfChar >= 49062) && (indexOfChar <= 49323))
                {

                    return 'K';

                }
                else if ((indexOfChar >= 49324) && (indexOfChar <= 49895))
                {

                    return 'L';

                }
                else if ((indexOfChar >= 49896) && (indexOfChar <= 50370))
                {

                    return 'M';

                }
                else if ((indexOfChar >= 50371) && (indexOfChar <= 50613))
                {

                    return 'N';

                }
                else if ((indexOfChar >= 50614) && (indexOfChar <= 50621))
                {

                    return 'O';

                }
                else if ((indexOfChar >= 50622) && (indexOfChar <= 50905))
                {

                    return 'P';

                }
                else if ((indexOfChar >= 50906) && (indexOfChar <= 51386))
                {

                    return 'Q';

                }
                else if ((indexOfChar >= 51387) && (indexOfChar <= 51445))
                {

                    return 'R';

                }
                else if ((indexOfChar >= 51446) && (indexOfChar <= 52217))
                {

                    return 'S';

                }
                else if ((indexOfChar >= 52218) && (indexOfChar <= 52697))
                {

                    return 'T';

                }
                else if ((indexOfChar >= 52698) && (indexOfChar <= 52979))
                {

                    return 'W';

                }
                else if ((indexOfChar >= 52980) && (indexOfChar <= 53688))
                {

                    return 'X';

                }
                else if ((indexOfChar >= 53689) && (indexOfChar <= 54480))
                {

                    return 'Y';

                }
                else if ((indexOfChar >= 54481) && (indexOfChar <= 55289))
                {

                    return 'Z';

                }
            }
            catch
            {

            }

            

            return null;
        }
    }
}
