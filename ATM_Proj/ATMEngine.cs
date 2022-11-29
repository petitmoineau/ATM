using System;
using System.Collections.Generic;
using System.Text;

namespace ATM
{
    class eATMEngine : Node
    {
        private eBank bankAcquire;
        eATM ATMowner;
        bool sessionIsOn = false;
        public eATMEngine(eATM _owner, eCommutator _commutator, eBank _bankAcquire)
            : base("ATM", _commutator)
        {
            this.ATMowner = _owner;
            bankAcquire = _bankAcquire;
            bankAcquire.ATMregister(this);
            init();
        }
        public void OnNewSession()//to use by interface
        {
            sessionIsOn = true;
        }
        public void SessionIsOver()//to use by interface
        {
            sessionIsOn = false;
            send(eLogger.GenerateLog(eUserAction.SESSION_OFF, "", bankAcquire.Name, Name, LogType.Req));
        }
        public void OnUserInput(eUserAction _action, string _userInput)//to use by interface
        {
            ProcessAction(_action, _userInput);
        }




        private bool ProcessAction(eUserAction _action, string _userInput)
        {
            send(eLogger.GenerateLog(_action, _userInput, bankAcquire.Name, Name, LogType.Req));
            return true;
        }

        public override void receive(eLog _payload)
        {
            if (_payload.Header.dst == Name)
            {
                if (_payload.Header.type == LogType.Req)//request
                {
                    //shit
                }
                else//answer
                {
                    //invokeInterface
                }
            }
            //Result of operation must be passed to interface!!!!!!!!!!!!!! CRITICAL
            //invoke interface method which notifies interface about answer
        } 
    }
}
