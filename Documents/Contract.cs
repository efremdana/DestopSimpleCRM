using System;

namespace Documents
{
    public class LIDContract 
    {
        public statusContract Status { get; set; }
        public DateTime Signing { get; private set; }
        public LIDContract()
        {
            Signing = DateTime.Now;
            Status = statusContract.Unsuccess;
        }
    }
}
