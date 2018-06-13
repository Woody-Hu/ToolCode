using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolCode
{
    public class ArrayUtility
    {
        /// <summary>
        /// 对一个容器进行随机查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="wantCount"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetRandomEnumerable<T>(IEnumerable<T> input,int wantCount)
        {
            if (null == input || input.Count() == 1 || wantCount <=0)
            {
                return input;
            }

            wantCount = Math.Min(wantCount,input.Count());

            return input.OrderBy(k => GetNewID<T>(k)).Take(wantCount);
        }

        /// <summary>
        /// 获得NewId
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        private static Guid GetNewID<T>(T inputValue)
        {
            return Guid.NewGuid();
        }
    }
}
