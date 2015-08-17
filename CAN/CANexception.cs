using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAN
{
    public class CANexception : Exception
    {
        string _errMsg;
        public CANexception(string msg)
        {
            _errMsg = msg;
        }

        public override string Message
        {
            get
            {
                return _errMsg;
            }
        }
    }
}
