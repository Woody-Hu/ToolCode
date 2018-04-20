using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace ToolCode
{
    /// <summary>
    /// 柱状图制作工具
    /// </summary>
    public class BarChartMaker:BaseChartMaker
    {

        /// <summary>
        /// 是否展示Y轴值
        /// </summary>
        private bool m_ifShowYValue = true;

        /// <summary>
        /// 是否展现Lables
        /// </summary>
        private bool m_ifShowLables = true;

        public BarChartMaker(int inputWidth = 500, int inputHight = 500, bool ifShowYValue = true, bool ifShowLables = true)
            : base(inputWidth, inputHight)
        {
            m_ifShowYValue = ifShowYValue;
            m_ifShowLables = ifShowLables;
        }

        /// <summary>
        /// 制作一个柱状图
        /// </summary>
        /// <param name="inputValues"></param>
        /// <param name="xTtitle"></param>
        /// <param name="yTitle"></param>
        /// <param name="title"></param>
        /// <param name="useXTags"></param>
        /// <returns></returns>
        public Image MakeBarChart(List<BarItemsRequest> inputValues,string xTtitle, string yTitle,string title, List<string> useXTags)
        {
            ZedGraphControl tempZ = new ZedGraphControl();

            tempZ.Width = m_useWidth;
            tempZ.Height = m_useHight;

            SetTitles(xTtitle, yTitle, title, tempZ);

            List<BarItem> lstUseBarITem = new List<BarItem>();

            //创建柱状图
            foreach (var oneRequest in inputValues)
            {
                lstUseBarITem.Add(tempZ.GraphPane.AddBar(oneRequest.UseLable, null, oneRequest.UseValue, oneRequest.UseColor));
            }

            //添加数值
            if (m_ifShowYValue)
            {
                foreach (var oneBaritem in lstUseBarITem)
                {
                    for (int i = 0; i < oneBaritem.Points.Count; i++)
                    {
                        //此处y值+1防止重叠
                        TextObj barLabel = new TextObj(oneBaritem.Points[i].Y.ToString(), 
                            oneBaritem.Points[i].X, oneBaritem.Points[i].Y +1
                            , CoordType.AxisXYScale, AlignH.Center, AlignV.Bottom);

                        barLabel.FontSpec.Border.IsVisible = false;
                        tempZ.GraphPane.GraphObjList.Add(barLabel);
                    }
                }

            }

            //检查是否做标签
            if (null != useXTags && 0 != useXTags.Count)
            {
                tempZ.GraphPane.XAxis.Scale.TextLabels = useXTags.ToArray();
                tempZ.GraphPane.XAxis.Type = AxisType.Text;
            }

            if (m_ifShowLables)
            {
                tempZ.GraphPane.Legend.IsVisible = false;
            }

            //调整轴
            tempZ.AxisChange();

            return tempZ.GetImage();
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="xTtitle"></param>
        /// <param name="title"></param>
        /// <param name="tempZ"></param>
        private static void SetTitles(string xTtitle,string yTitle, string title, ZedGraphControl tempZ)
        {
            //若需要设标题
            if (!string.IsNullOrWhiteSpace(title))
            {
                tempZ.GraphPane.Title.Text = title;
                tempZ.GraphPane.Title.IsVisible = true;
            }
            else
            {
                tempZ.GraphPane.Title.IsVisible = false;
            }

            //x轴标题
            if (!string.IsNullOrWhiteSpace(xTtitle))
            {
                tempZ.GraphPane.XAxis.Title.Text = xTtitle;
                tempZ.GraphPane.XAxis.Title.IsVisible = true;
            }
            else
            {
                tempZ.GraphPane.XAxis.Title.IsVisible = false;
            }

            //y轴标题
            if (!string.IsNullOrWhiteSpace(yTitle))
            {
                tempZ.GraphPane.YAxis.Title.Text = yTitle;
                tempZ.GraphPane.YAxis.Title.IsVisible = true;
            }
            else
            {
                tempZ.GraphPane.YAxis.Title.IsVisible = false;
            }
        }

        /// <summary>
        /// 使用的BarItem请求
        /// </summary>
        public class BarItemsRequest
        {
            public Color UseColor { set; get; }

            public string UseLable { set; get; }

            public double[] UseValue { set; get; }
        }

    }
}
