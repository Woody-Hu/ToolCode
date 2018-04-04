using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPOIUtility
{
    public class ReadManger
    {
        public List<Dictionary<string, string>> ReadByxlsx(string inputPath)
        {
            XSSFWorkbook useWorkbook = null;

            using (FileStream useFs = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                useWorkbook = new XSSFWorkbook(useFs);
            }

            var useSheet = useWorkbook.GetSheetAt(0);

            var rowEnumerator = useSheet.GetRowEnumerator();
            rowEnumerator.MoveNext();
            var useRow = (XSSFRow)rowEnumerator.Current;

            Dictionary<string, int> useDicMap = new Dictionary<string, int>();

            for (int columnIndex = 0; columnIndex < useRow.LastCellNum; columnIndex++)
            {
                var useCell = useSheet.GetRow(0).GetCell(columnIndex);
                var cellString = useCell.ToString();

                if (!string.IsNullOrWhiteSpace(cellString) && !useDicMap.ContainsKey(cellString))
                {
                    useDicMap.Add(cellString, columnIndex);
                }
            }

            List<Dictionary<string, string>> returnValue = new List<Dictionary<string, string>>();


            while (rowEnumerator.MoveNext())
            {
                var nowRow = (XSSFRow)rowEnumerator.Current;

                Dictionary<string, string> tempDic = new Dictionary<string, string>();

                foreach (var oneKVP in useDicMap)
                {
                    var useCell = nowRow.GetCell(oneKVP.Value);

                    if (null != useCell)
                    {
                        tempDic.Add(oneKVP.Key, useCell.ToString());
                    }
                    else
                    {
                        tempDic.Add(oneKVP.Key, string.Empty);
                    }

                }

                returnValue.Add(tempDic);
            }

            return returnValue;
        }
    }
}
