using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
   
    class ePaymentSystem : Node
    {
        Dictionary<string, string> cardsCodes;//create dictionary<string, list<string>>
        public ePaymentSystem(eCommutator _commutator)
            : base("PAYMENT_SYSTEM", _commutator)
        {
            cardsCodes = new Dictionary<string, string>();
            init();

            cardsCodes.Add("PrivatBank", "4441");
        }

        public override void receive(eLog payload)
        {
            Process(payload);
        }

        private bool Process(eLog payload)
        {
            if (payload.Header.dst == Name)
            {
                if (payload.Header.type == LogType.Req)
                {
                    ReqSenders.Push(payload.Header.src);
                    var bankEmitent = cardsCodes.First(i => i.Value == payload.Data.Substring(0, 4)).Key;//must be a database query?
                    send(eLogger.GenerateLog(payload.Header.action, payload.Data, bankEmitent, Name, payload.Header.type));//change for exact bank then
                }
                else
                {
                    send(eLogger.GenerateLog(payload.Header.action, payload.Data, ReqSenders.Pop(), Name, payload.Header.type));
                }
                return true;
            }


            return false;
        }
    }
}
