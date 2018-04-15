using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolCode
{
     /// <summary>
    /// 颜色获得器
    /// </summary>
    public class ColorsGeter
    {
        /// <summary>
        /// 使用的颜色获得器
        /// </summary>
        private static List<Color> m_lstSystemColor = new List<Color>();

        /// <summary>
        /// 静态初始化
        /// </summary>
        static ColorsGeter()
        {
            Type useType = typeof(Color);

            foreach (var oneProperty in useType.GetProperties
                (System.Reflection.BindingFlags.Public|System.Reflection.BindingFlags.Static))
            {
                if (oneProperty.CanRead && oneProperty.PropertyType == useType)
                {
                    var useColor = (Color)oneProperty.GetValue(null);
                    if (useColor != Color.White && useColor != Color.Transparent && useColor != Color.Black)
                    {
                        m_lstSystemColor.Add(useColor);
                    }

                }
            }
            m_lstSystemColor = m_lstSystemColor.OrderBy(k => -Math.Max(k.R, Math.Max(k.G, k.B))).ThenBy(k=>k.R + k.G +k.B).ToList();
        }

        /// <summary>
        /// 获得使用的颜色
        /// </summary>
        /// <param name="count"></param>
        /// <param name="useColors"></param>
        /// <returns></returns>
        public bool GetColors(int count,out List<Color> useColors)
        {
            useColors = null;

            if (0 >= count ||  count > m_lstSystemColor.Count)
            {
                return false;
            }

            Color[] retrunColors = new Color[count];

            m_lstSystemColor.CopyTo(0,retrunColors, 0, count);
            useColors = retrunColors.ToList();

            return true;
        }
    }
}
