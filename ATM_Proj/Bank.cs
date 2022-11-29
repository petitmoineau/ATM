using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    abstract class eBank : Node
    {
        public readonly string _identifier = "0";
        public List<eATMEngine> ATMNetwork { get; set; }
        public eBank(string name, eCommutator _commutator)
            : base(name, _commutator)
        {}

        public abstract bool ATMregister(eATMEngine newATM);
        protected abstract Result ProcessQuery(string data);
    }

    class ePrivatBank : eBank 
    {
        private eBankUser CurrentUser { get; set; }
        public ePrivatBank(eCommutator _commutator)
            : base("PrivatBank", _commutator)
        {
            ATMNetwork = new List<eATMEngine>();
            init();
        }
        public override bool ATMregister(eATMEngine newATM)
        {
            ATMNetwork.Add(newATM);
            return true;
        }

        public override void receive(eLog payload)
        {
            Process(payload);
        }

        private bool Process(eLog payload)
        {
            if (payload.Header.dst == Name)
            {
                if (payload.Header.type == LogType.Req)//request
                {
                    ReqSenders.Push(payload.Header.src);
                    if (ReqSenders.Peek() == "PAYMENT_SYSTEM")
                    {
                        if (payload.Header.action == eUserAction.CREDIT_CARD_INSERTED) { CurrentUser = new eBankUser(); }
                        else if (payload.Header.action == eUserAction.SESSION_OFF) { CurrentUser = null; return true; }

                        send(eLogger.GenerateLog(payload.Header.action, utils.ResultToString(ProcessQuery(payload.Data)), ReqSenders.Pop(), Name, LogType.Ack));
                    }
                    else
                        send(eLogger.GenerateLog(payload.Header.action, payload.Data, "PAYMENT_SYSTEM", Name, payload.Header.type));
                }
                else//answer
                {
                    send(eLogger.GenerateLog(payload.Header.action, payload.Data, ReqSenders.Pop(), Name, payload.Header.type));
                }
            }
            return true;
        }

        protected override Result ProcessQuery(string data)
        {
            //query impl
            return Result.SUCCESS;
        }
    }
}
