using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTOperator.Util.Log;

namespace CAN
{
    public class CANFrameInfoArgs : EventArgs
    {
        /// <summary>
        /// CAN 的接收帧
        /// </summary>
        FrameInfo _canFrameInfo = new FrameInfo("1", "1", "1", "1", "1");
        internal FrameInfo CanFrameInfo
        {
            get { return _canFrameInfo; }
        }

        public CANFrameInfoArgs(
            string frameID,
            string timeStamp,
            string frameFormat,
            string frameType,
            string data)
        {
            try
            {
                if (frameID.Equals(string.Empty) &&
                timeStamp.Equals(string.Empty) &&
                frameFormat.Equals(string.Empty) &&
                frameType.Equals(string.Empty) &&
                data.Equals(String.Empty))
                {
                    throw new CANexception("CANFrameInfo 参数为空");
                }
                else
                {
                    _canFrameInfo.FrameID = frameID;
                    _canFrameInfo.TimeStamp = timeStamp;
                    _canFrameInfo.FrameFormat = frameFormat;
                    _canFrameInfo.FrameType = frameType;
                    _canFrameInfo.Data = data;
                }
            }
            catch (CANexception e)
            {
                LogHelper.WriteErrorLog(e.Message);
            }
        }

        public CANFrameInfoArgs(FrameInfo frame)
        {
            try
            {
                if (frame == null)
                {
                    throw new CANexception("CANFrameInfoArgs 参数不能为空");
                }
                //_canFrameInfos = frameInfos;
                _canFrameInfo = frame;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(e.Message);
            }
        }
    }
}
