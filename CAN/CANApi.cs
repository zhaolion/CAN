using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CAN
{
    public sealed class CANApi
    {
        //接口卡类型定义
        public enum PCIDeviceType
        {
            VCI_PCI5121 = 1,
            VCI_PCI9810 = 2,
            VCI_USBCAN1 = 3,
            VCI_USBCAN2 = 4,
            VCI_PCI9820 = 5,
            VCI_CAN232 = 6,
            VCI_PCI5110 = 7,
            VCI_CANLITE = 8,
            VCI_ISA9620 = 9,
            VCI_ISA5420 = 10,
            VCI_PC104CAN = 11,
            VCI_CANETE = 12,
            VCI_DNP9810 = 13,
            VCI_PCI9840 = 14,
            VCI_PCI9820I = 16
        }

        //函数调用返回状态值
        public static readonly int STATUS_OK = 1;
        public static readonly int STATUS_ERR = 0;

        public enum ErrorType
        {
            //CAN错误码
            ERR_CAN_OVERFLOW = 0x0001,	//CAN控制器内部FIFO溢出
            ERR_CAN_ERRALARM = 0x0002,	//CAN控制器错误报警
            ERR_CAN_PASSIVE = 0x0004,	//CAN控制器消极错误
            ERR_CAN_LOSE = 0x0008,	//CAN控制器仲裁丢失
            ERR_CAN_BUSERR = 0x0010,	//CAN控制器总线错误

            //通用错误码
            ERR_DEVICEOPENED = 0x0100,	//设备已经打开
            ERR_DEVICEOPEN = 0x0200,	//打开设备错误
            ERR_DEVICENOTOPEN = 0x0400,	//设备没有打开
            ERR_BUFFEROVERFLOW = 0x0800,	//缓冲区溢出
            ERR_DEVICENOTEXIST = 0x1000,	//此设备不存在
            ERR_LOADKERNELDLL = 0x2000,	//装载动态库失败
            ERR_CMDFAILED = 0x4000,	//执行命令失败错误码
            ERR_BUFFERCREATE = 0x8000	//内存不足

        }



        //1.ZLGCAN系列接口卡信息的数据类型。
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct VCI_BOARD_INFO
        {
            public ushort hw_Version;
            public ushort fw_Version;
            public ushort dr_Version;
            public ushort in_Version;
            public ushort irq_Num;
            public byte can_Num;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 20)]
            public string str_Serial_Num;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 40)]
            public string str_hw_Type;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U2)]
            public ushort[] Reserved;
        }

        //2.定义CAN信息帧的数据类型。
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct VCI_CAN_OBJ
        {
            public uint ID;
            public uint TimeStamp;
            public byte TimeFlag;
            public byte SendType;
            public byte RemoteFlag;//是否是远程帧
            public byte ExternFlag;//是否是扩展帧
            public byte DataLen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] Data;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] Reserved;
        }

        //3.定义CAN控制器状态的数据类型。
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct VCI_CAN_STATUS
        {
            public byte ErrInterrupt;
            public byte regMode;
            public byte regStatus;
            public byte regALCapture;
            public byte regECCapture;
            public byte regEWLimit;
            public byte regRECounter;
            public byte regTECounter;
            public uint Reserved;
        }

        //4.定义错误信息的数据类型。
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct VCI_ERR_INFO
        {
            public uint ErrCode;

            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] Passive_ErrData;

            public byte ArLost_ErrData;
        }

        //5.定义初始化CAN的数据类型
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct VCI_INIT_CONFIG
        {
            public uint AccCode;
            public uint AccMask;
            public uint Reserved;
            public byte Filter;
            public byte Timing0;
            public byte Timing1;
            public byte Mode;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct CHGDESIPANDPORT
        {
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 10)]
            public string szpwd;

            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 20)]
            public string szdesip;

            public int desport;
        }

        #region API函数

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_OpenDevice", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_OpenDevice(uint DeviceType, uint DeviceInd, uint Reserved);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_CloseDevice", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_CloseDevice(uint DeviceType, uint DeviceInd);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_InitCAN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_InitCAN(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_INIT_CONFIG pInitConfig);


        [DllImport("ControlCAN.dll", EntryPoint = "VCI_ReadBoardInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_ReadBoardInfo(uint DeviceType, uint DeviceInd, ref VCI_BOARD_INFO pInfo);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_ReadErrInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_ReadErrInfo(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_ERR_INFO pErrInfo);


        [DllImport("ControlCAN.dll", EntryPoint = "VCI_ReadCANStatus", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_ReadCANStatus(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_CAN_STATUS pCANStatus);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_GetReference", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_GetReference(uint DeviceType, uint DeviceInd, uint CANInd, uint RefType, object pData);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_SetReference", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_SetReference(uint DeviceType, uint DeviceInd, uint CANInd, uint RefType, object pData);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_GetReceiveNum", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_GetReceiveNum(uint DeviceType, uint DeviceInd, uint CANInd);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_ClearBuffer", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_ClearBuffer(uint DeviceType, uint DeviceInd, uint CANInd);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_StartCAN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_StartCAN(uint DeviceType, uint DeviceInd, uint CANInd);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_ResetCAN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_ResetCAN(uint DeviceType, uint DeviceInd, uint CANInd);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_Transmit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_Transmit(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_CAN_OBJ pSend, uint Len);

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_Receive", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern uint VCI_Receive(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_CAN_OBJ pReceive, uint Len, int WaitTime);


        #endregion
    }
}
