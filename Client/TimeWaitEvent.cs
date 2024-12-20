using System;

namespace Clients
{
    public delegate void TimeWaitEventHandler(object sender, TimeWaitEventArgs e);
    public class TimeWaitEventArgs : EventArgs
    {
        public int TimeWait { get; private set; }

        public TimeWaitEventArgs(int timeWait) : base()
        { 
            TimeWait = timeWait;
        }
    }
}
