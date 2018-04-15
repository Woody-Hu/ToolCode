using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPOIUtility
{
    /// <summary>
    /// 图片插入管理器
    /// </summary>
    public class PictureManger
    {
        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="inputSheet"></param>
        /// <param name="lstInputRequest"></param>
        /// <returns></returns>
        public static bool TryInsertPictures(ISheet inputSheet,List<PictureInsertRequest> lstInputRequest)
        {
            try
            {
                //获取/创建
                IDrawing patriarch = inputSheet.DrawingPatriarch;

                if (null == patriarch)
                {
                    patriarch = inputSheet.CreateDrawingPatriarch();
                }

                foreach (var oneRequest in lstInputRequest)
                {
                    //第一步：将图片读入二进制数组
                    MemoryStream mStream = new MemoryStream();
                    oneRequest.UseImage.Save(mStream, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] bytePic = mStream.GetBuffer();

                    //第二步：图片加载进workbook
                    int pictureIdx = inputSheet.Workbook.AddPicture(bytePic, PictureType.PNG);

                    //第三步：设置锚点  
                    IClientAnchor anchor = patriarch.CreateAnchor(0, 0, 0, 0, oneRequest.StartColumn, oneRequest.StartRow, oneRequest.EndColumn, oneRequest.EndRow);

                    //第四步：创建图片  
                    IPicture pict = patriarch.CreatePicture(anchor, pictureIdx);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }        
        }
    }
}
