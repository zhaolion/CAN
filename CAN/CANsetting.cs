using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTOperator.Util.Log;
using log4net;

namespace CAN
{
    public class CANsetting
    {

        #region CAN internal setting
        /// 设备索引号
        private uint _deviceIndex = 0;

        public uint DeviceIndex
        {
            get { return _deviceIndex; }
        }
        /// CAN路数索引号
        uint _canIndex = 0;

        public uint CanIndex
        {
            get { return _canIndex; }
        }
        /// CAN过滤设置
        byte _filterType = 0;

        public byte FilterType
        {
            get { return _filterType; }
        }
        /// CAN模式
        byte _canMode = 0;

        public byte CanMode
        {
            get { return _canMode; }
        }
        /// 验收码
        uint _canCode = 0;

        public uint CanCode
        {
            get { return _canCode; }
        }
        /// CAN屏蔽码
        uint _canMask = 0;

        public uint CanMask
        {
            get { return _canMask; }
        }
        /// 定时器0
        uint _canTime0 = 0;

        public uint CanTime0
        {
            get { return _canTime0; }
        }
        /// 定时器1
        uint _canTime1 = 0;

        public uint CanTime1
        {
            get { return _canTime1; }
        }

        CANApi.VCI_INIT_CONFIG _canConfig = new CANApi.VCI_INIT_CONFIG();

        public CANApi.VCI_INIT_CONFIG CanConfig
        {
            get { return _canConfig; }
        }
        /// <summary>
        /// 获取CAN的配置
        /// </summary>
        /// <returns></returns>
        public CANApi.VCI_INIT_CONFIG GetConfig()
        {
            return _canConfig;
        }

        #endregion

        #region CAN Default setting
        string _canCodeDefault = "00000000";
        string _canMaskDefault = "FFFFFFFF";
        string _canTime0Default = "4F";
        string _canTime1Default = "2F";
        #endregion

        #region 设置CAN属性的API
        #region CAN属性设置
        /// <summary>
        /// 设置CAN设备号
        /// </summary>
        /// <param name="index">CAN设备号</param>
        private void SetDeviceIndex(uint index)
        {
            _deviceIndex = index;
        }
        /// <summary>
        /// 设置CAN路数
        /// </summary>
        /// <param name="index">CAN路数</param>
        private void SetCanIndex(byte index)
        {
            _canIndex = index;
        }
        /// <summary>
        /// 设置过滤类型
        /// </summary>
        /// <param name="type">过滤类型</param>
        private void SetFilterType(byte type)
        {
            _filterType = type;
        }
        /// <summary>
        /// 设置CAN模式
        /// </summary>
        /// <param name="mode">CAN模式</param>
        private void SetCanMode(byte mode)
        {
            _canMode = mode;
        }
        /// <summary>
        /// 设置CAN 验收码
        /// </summary>
        /// <param name="canCodeStr">CAN Code</param>
        private void SetCanCode(string canCodeStr)
        {
            try
            {
                if (!uint.TryParse(
                    canCodeStr,
                    System.Globalization.NumberStyles.AllowHexSpecifier,
                    null,
                    out _canCode))
                {
                    throw new CANexception("验收码格式错误");
                }

            }
            catch (CANexception e)
            {
                LogHelper.WriteErrorLog(e.Message);
            }

        }
        /// <summary>
        /// 设置CAN掩码
        /// </summary>
        /// <param name="canMaskStr">CAN掩码</param>
        private void SetCanMask(string canMaskStr)
        {
            try
            {
                if (!uint.TryParse(
                    canMaskStr,
                    System.Globalization.NumberStyles.AllowHexSpecifier,
                    null,
                    out _canMask))
                {
                    throw new CANexception("掩码格式错误");
                }

            }
            catch (CANexception e)
            {
                LogHelper.WriteErrorLog(e.Message);
            }
        }
        /// <summary>
        /// 设置CAN计时器0
        /// </summary>
        /// <param name="time0">CAN计时器0</param>
        private void SetTime0(string time0)
        {
            try
            {
                if (!uint.TryParse(
                    time0,
                    System.Globalization.NumberStyles.AllowHexSpecifier,
                    null,
                    out _canTime0))
                {
                    throw new CANexception("定时器0格式错误");
                }

            }
            catch (CANexception e)
            {
                LogHelper.WriteErrorLog(e.Message);
            }
        }
        /// <summary>
        /// 设置CAN计时器1
        /// </summary>
        /// <param name="time1">CAN计时器0</param>
        private void SetTime1(string time1)
        {
            try
            {
                if (!uint.TryParse(
                    time1,
                    System.Globalization.NumberStyles.AllowHexSpecifier,
                    null,
                    out _canTime1))
                {
                    throw new CANexception("定时器1格式错误");
                }
            }
            catch (CANexception e)
            {
                LogHelper.WriteErrorLog(e.Message);
            }
        }
        /// <summary>
        /// 配置Config
        /// </summary>
        void SetConfig()
        {
            _canConfig.AccCode = _canCode;
            _canConfig.AccMask = _canMask;
            _canConfig.Filter = _filterType;
            _canConfig.Mode = _canMode;
            _canConfig.Timing0 = (byte)_canTime0;
            _canConfig.Timing1 = (byte)_canTime1;
        }
        #endregion

        #region 常规设置
        /// <summary>
        /// 默认设置
        /// </summary>
        public void DefaultSetting()
        {
            try
            {
                SetDeviceIndex(0);
                SetCanIndex(0);
                SetFilterType(0);
                SetCanMode(0);
                SetCanCode(_canCodeDefault);
                SetCanMask(_canMaskDefault);
                SetTime0(_canTime0Default);
                SetTime1(_canTime1Default);
                SetConfig();
            }
            catch (CANexception e)
            {
                LogHelper.WriteErrorLog(e.Message);
            }
        }
        /// <summary>
        /// 自定义设置
        /// </summary>
        /// <param name="deviceIndex">设备号</param>
        /// <param name="canIndex">CAN路数</param>
        /// <param name="filterType">过滤类型</param>
        /// <param name="canMode">CAN模式</param>
        /// <param name="canCode">验收码</param>
        /// <param name="canMask">掩码</param>
        /// <param name="canTime0">定时器0</param>
        /// <param name="canTime1">定时器1</param>
        public void SetCAN(
            uint deviceIndex = 0,
            byte canIndex = 0,
            byte filterType = 0,
            byte canMode = 0,
            string canCode = "00000000",
            string canMask = "FFFFFFFF",
            string canTime0 = "4F",
            string canTime1 = "2F")
        {
            try
            {
                SetDeviceIndex(deviceIndex);
                SetCanIndex(canIndex);
                SetFilterType(filterType);
                SetCanMode(canMode);
                SetCanCode(canCode);
                SetCanMask(canMask);
                SetTime0(canTime0);
                SetTime1(canTime1);
                SetConfig();
                OnConfigChanged();
            }
            catch (CANexception e)
            {
                LogHelper.WriteErrorLog(e.Message);
            }
        }
        #endregion
        #endregion

        #region Config Changed
        public event EventHandler<ConfigChangeArgs> ConfigChanged;

        private void OnConfigChanged()
        {
            EventHandler<ConfigChangeArgs> temp = ConfigChanged;
            ConfigChangeArgs e = new ConfigChangeArgs();
            if (temp != null)
            {
                temp(this, e);
            }
        }
        #endregion

        public CANsetting()
        {
            DefaultSetting();
        }
    }

    public class ConfigChangeArgs : EventArgs { }
}
