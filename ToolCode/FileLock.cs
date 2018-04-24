using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ToolCode
{
    /// <summary>
    /// 文件锁
    /// </summary>
    public class FileLock
    {
        private string m_useFileName = "FileLocker";

        private string m_useAppendName = ".locker";

        DirectoryInfo m_useDirectionInfo;

        /// <summary>
        /// 构造文件锁
        /// </summary>
        /// <param name="inputName"></param>
        public FileLock(string inputName)
        {
            if (!string.IsNullOrWhiteSpace(inputName))
            {
                m_useFileName = inputName;
            }

            FileInfo nowFileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);

            m_useDirectionInfo = nowFileInfo.Directory;
        }

        /// <summary>
        /// 获得锁
        /// </summary>
        public void GetFileLock()
        {
            string useLockerPath = m_useDirectionInfo.FullName + @"\" + m_useFileName + m_useAppendName;

            Random useRandom = new Random();

            //双重检查锁
            while (File.Exists(useLockerPath))
            {
                Thread.Sleep(useRandom.Next(0,50));

                while (File.Exists(useLockerPath))
                {
                    Thread.Sleep(useRandom.Next(0, 30));
                }
            }

            //创建锁
            using (StreamWriter fs = new StreamWriter(new FileStream(useLockerPath,FileMode.CreateNew,FileAccess.Write)))
            {
                fs.WriteLine(m_useFileName);
            }

            return;
        }

        public void ReleasFileLock()
        {
            string useLockerPath = m_useDirectionInfo.FullName + @"\" + m_useFileName + m_useAppendName;

            if (File.Exists(useLockerPath))
            {
                File.Delete(useLockerPath);
            }

        }
    }
}
