using System;
using System.Collections.Generic;
using System.Text;

namespace CAN
{
    public class FrameInfo
    {
        public FrameInfo(string frameID, string timeStamp, string frameFormat, string frameType, string data)
        {
            this.FrameID = frameID;
            this.TimeStamp = timeStamp;
            this.FrameFormat = frameFormat;
            this.FrameType = frameType;
            this.Data = data;
        }

        private string mFrameID;
        /// <summary>
        /// 帧ID
        /// </summary>
        public string FrameID
        {
            get { return this.mFrameID; }
            set { this.mFrameID = value; }
        }

        private string mTimeStamp;
        /// <summary>
        /// 时间标识
        /// </summary>
        public string TimeStamp
        {
            get { return this.mTimeStamp; }
            set { this.mTimeStamp = value; }
        }

        private string mFrameFormat;
        /// <summary>
        /// 帧格式
        /// </summary>
        public string FrameFormat
        {
            get { return this.mFrameFormat; }
            set { this.mFrameFormat = value; }
        }

        private string mFrameType;
        /// <summary>
        /// 帧类型
        /// </summary>
        public string FrameType
        {
            get { return this.mFrameType; }
            set { this.mFrameType = value; }
        }

        private string mData;
        /// <summary>
        /// 数据
        /// </summary>
        public string Data
        {
            get { return this.mData; }
            set { this.mData = value; }
        }
        public override string ToString()
        {
            return string.Format("帧ID={0}  时间标识={1,-10}  帧格式={2,-8}  帧类型={3,-8}  数据={4,-30}", this.FrameID, this.TimeStamp, this.FrameFormat, this.FrameType, this.Data);
        }
    }

    /// <summary>
    /// 帧类型
    /// StandardFrame = 0 标准帧
    /// ExtendedFrame = 1 扩展帧
    /// </summary>
    public enum CanFrameType
    {
        StandardFrame = 0, //标准帧
        ExtendedFrame = 1 //扩展帧
    };
    /// <summary>
    /// 帧格式
    /// DataFrame 数据帧
    /// RemoteFrame 远程帧
    /// </summary>
    public enum CanFrameFormat
    {
        DataFrame = 0,
        RemoteFrame = 1
    };
    /// <summary>
    /// 滤波模式
    /// DualFilter 双滤波
    /// SingleFilter 单滤波
    /// </summary>
    public enum CanFilterType
    {
        DualFilter = 0,
        SingleFilter = 1
    };
    /// <summary>
    /// CAN监听模式
    /// NormalMode 正常模式
    /// ListenOnlyMode 只听模式
    /// </summary>
    public enum CanMode { NormalMode = 0, ListenOnlyMode = 1 };
    /// <summary>
    /// CAN发送类型
    /// NormalTransmission 正常发送
    /// SingleTransmission 单次发送
    /// SelfTransmissionSelfRecevied 自发自收
    /// SingleSelfTransmissionSelfRecevied 单次自发自收
    /// </summary>
    public enum FrameSendType
    {
        NormalTransmission = 0,
        SingleTransmission = 1,
        SelfTransmissionSelfRecevied = 2,
        SingleSelfTransmissionSelfRecevied = 3
    };

}
