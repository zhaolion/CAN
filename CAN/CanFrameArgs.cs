using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAN
{
    public class CanFrameArgs : EventArgs
    {
        FrameInfo _canFrame = new FrameInfo("1", "1", "1", "1", "1");

        public FrameInfo CanFrame
        {
            get { return _canFrame; }
            set { _canFrame = value; }
        }

        public CanFrameArgs() { }

        public CanFrameArgs(FrameInfo frame)
        {
            _canFrame = frame;
        }

    }
}
