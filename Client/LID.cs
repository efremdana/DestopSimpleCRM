using Documents;
using System;
using System.ComponentModel;
using System.Threading;

namespace Clients
{
    public class LID : ILID, IDisposable
    {
        public string Name { get; set; }

        public int Number { get; set; }

        public Sell Sell { get; set; }
        private BackgroundWorker contractWaitingTamer;

        public LID(string name, int number)
        {
            Name = name;
            Number = number;
            Sell = new Sell(new LIDContract());
        }

        public virtual void Dispose()
        {
            if (contractWaitingTamer != null) contractWaitingTamer.Dispose();
        }

        public override string ToString()
        {
            return $"{Number} Лид {Name}";
        }

        public event TimeWaitEventHandler TimeWait;
        public event EventHandler ContractSigned; 

        private void CountTimer(int currentTime)
        {
            if (TimeWait != null)
            {
                TimeWait(this, new TimeWaitEventArgs(currentTime));
            }
        }

        private void FinishTimer()
        {
            if (ContractSigned != null)
                ContractSigned(this, null);
        }

        public void StartWaitingContract()
        {
            contractWaitingTamer = new BackgroundWorker();
            contractWaitingTamer.WorkerReportsProgress = true;
            contractWaitingTamer.WorkerSupportsCancellation = true;
            int currentTime = 5;
            contractWaitingTamer.DoWork += (o, args) =>
            {
                while (currentTime > 0)
                {
                    Thread.Sleep(1000);
                    contractWaitingTamer.ReportProgress(0);
                    currentTime--;
                }
                FinishTimer();
            };
            contractWaitingTamer.ProgressChanged += (o, args) =>
            {
                CountTimer(currentTime);
            };
            contractWaitingTamer.RunWorkerAsync();
        }

    }
}
