using System;

namespace Clients
{
    interface ILID
    {
        event TimeWaitEventHandler TimeWait;
        event EventHandler ContractSigned;
        void StartWaitingContract();
    }
}
