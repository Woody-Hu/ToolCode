using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ToolCode
{
    /// <summary>
    /// 互斥进程锁
    /// </summary>
    public class MutexLock
    {
        Mutex m_useLock;

        public MutexLock( string useName)
        {
            m_useLock = new Mutex(false, useName);
        }

        public void LockAction(Action inputAction)
        {
            try
            {
                m_useLock.WaitOne();
                inputAction();
            }
            finally
            {
                m_useLock.ReleaseMutex();
            }
        }


    }
}
