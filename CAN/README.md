# CAN
一个对ZLG CAN 的C# 包装库
#简单说明
Version: 1.0 2015.8.17<br>
CAN type：周立功CAN USB2（ZLG USB2）<br>

使用范例：<br>

CANhelper _can = new CANhelper();<br>

_can.CanSetting.SetCAN(
                (uint)0,	//deviceIndex 设备索引号<br>
				(byte)0,	//canIndex CAN的路索引号<br>
                (byte)CanFilterType.DualFilter,		//filterType 过滤类型<br>
                (byte)CanMode.NormalMode,	//canMode CAN模式<br>
				"00000000",		//canCode 过滤码 <br>
				"FFFFFFFF",		//canMask 掩码<br>
				"4F",	//时间高位<br>
				"2F");	//时间低位,默认 4F 2F<br>

_can.ConnectCANDevice(); //连接CAN设备<br>
_can.StartCAN();		//启动CAN设备<br>
_can.CloseCANDevice();	//关闭CAN设备<br>
_can.ResetCANDevice();	//复位CAN设备<br>

如何接收收到的数据？<br>
答案：订阅ReceviedData事件，取CANFrameInfoArgs.CanFrameInfo，就是收到的CAN帧，数据格式是FrameInfo（具体看源码）<br>

