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
    /// 饼状图制作器
    /// </summary>
    public class PieChartMaker:BaseChartMaker
    {
        public PieChartMaker(int inputWidth = 500, int inputHight = 500):base(inputWidth, inputHight)
        {

        }

        /// <summary>
        /// 制作饼状图
        /// </summary>
        /// <param name="lstInputRequest"></param>
        /// <param name="inputTitle"></param>
        /// <returns></returns>
        public Image MakePieChart(List<PieChartRequest> lstInputRequest,string inputTitle = null)
        {
            ZedGraphControl tempZ = new ZedGraphControl();
            tempZ.Width = m_useWidth;
            tempZ.Height = m_useHight;

            var useGraph = tempZ.GraphPane;

            useGraph.Legend.IsVisible = false;
            useGraph.XAxis.IsVisible = false;
            useGraph.YAxis.IsVisible = false;
            useGraph.Legend.Position = LegendPos.InsideTopLeft;
            useGraph.Legend.FontSpec.Size = 15f;
            useGraph.Legend.IsHStack = false;
            useGraph.Title.IsVisible = false;

            if (!string.IsNullOrWhiteSpace(inputTitle))
            {
                useGraph.Title.Text = inputTitle;
                useGraph.Title.IsVisible = true;
            }

            foreach (var oneRequest in lstInputRequest)
            {
                var tempSegment = useGraph.AddPieSlice(oneRequest.Value, oneRequest.UseColor, oneRequest.Offset, oneRequest.Name);
                tempSegment.LabelDetail.FontSpec.Size = 15f;
                tempSegment.Border.IsVisible = false;
                tempSegment.LabelType = PieLabelType.Name_Value_Percent;
 
            }

            return tempZ.GetImage();

        }

        /// <summary>
        /// 饼状图制作请求
        /// </summary>
        public class PieChartRequest
        {
            /// <summary>
            /// 对应的名字
            /// </summary>
            public string Name { set; get; }

            /// <summary>
            /// 对应的值
            /// </summary>
            public double Value { set; get; }

            /// <summary>
            /// 对应的颜色
            /// </summary>
            public Color UseColor { set; get; }

            /// <summary>
            /// 对应的偏移值
            /// </summary>
            public double Offset { set; get; }
        }
    }
}
