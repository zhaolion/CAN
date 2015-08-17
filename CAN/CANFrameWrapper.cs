using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTOperator.Util.Log;

namespace CAN
{
    class CANFrameWrapper
    {
        /// <summary>
        /// 根据输入包装一个CAN帧
        /// </summary>
        /// <param name="frameID"></param>
        /// <param name="timeStamp"></param>
        /// <param name="frameFormat"></param>
        /// <param name="frameType"></param>
        /// <param name="frameData"></param>
        /// <param name="frameSendType"></param>
        /// <returns></returns>
        public CANApi.VCI_CAN_OBJ[] Wrapper(string frameID, string timeStamp,
            int frameFormat, int frameType, string frameData,
            int frameSendType)
        {
            try
            {
                CANApi.VCI_CAN_OBJ[] frameInfo = new CANApi.VCI_CAN_OBJ[1];
                //数据帧中数据不能为空
                if (frameData.Length == 0 && frameType == 0)
                {
                    LogHelper.WriteErrorLog("帧数据的长度不能为0");
                    return frameInfo;
                }
                else if (frameID.Length == 0)
                {
                    LogHelper.WriteErrorLog("帧ID的长度不能为0");
                    return frameInfo;
                }
                else if (frameID.Length > 8)
                {
                    LogHelper.WriteErrorLog("帧ID的值超过范围");
                    return frameInfo;
                }
                else if (frameData.Length > 24)
                {
                    LogHelper.WriteErrorLog("数据长度超过范围，最大长度8个字节");
                    return frameInfo;
                }
                else if (frameData.Length % 3 == 1 && frameType == 0)
                {
                    LogHelper.WriteErrorLog("帧数据格式不正确");
                    return frameInfo;
                }
                else
                {
                    string strData = frameData;

                    string[] ss = strData.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    byte[] data = new byte[8];
                    for (int i = 0; i < 8; i++)
                    {
                        if (ss.Length <= i) break;
                        byte b = 0;
                        if (!byte.TryParse(ss[i], System.Globalization.NumberStyles.AllowHexSpecifier,
                            null, out b))
                            b = 0;
                        data[i] = b;
                    }

                    frameInfo[0].DataLen = (byte)(ss.Length);
                    frameInfo[0].Data = data;
                    frameInfo[0].Reserved = new byte[3];
                    frameInfo[0].RemoteFlag = (byte)frameFormat;
                    frameInfo[0].ExternFlag = (byte)frameType;

                    uint canFrameID = 0;
                    if (!uint.TryParse(frameID,
                        System.Globalization.NumberStyles.AllowHexSpecifier, null, out canFrameID))
                        canFrameID = 0;
                    if (frameInfo[0].ExternFlag == 1)
                    {
                        frameInfo[0].ID = canFrameID;
                    }
                    else
                    {
                        frameInfo[0].ID = canFrameID | 0x0000FFFF;
                    }

                    frameInfo[0].SendType = (byte)frameSendType;

                }

                return frameInfo;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(e.Message);
                throw e;
            }

        }
    }
}
