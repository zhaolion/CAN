using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAN
{
    /// <summary>
    /// CANFilter:用于过滤CAN数据的过滤器类
    /// 过滤约束最为严格
    /// 只过滤8个byte的数据
    /// </summary>
    public class CANFilter
    {
        byte[] filterFlagBytes = new byte[8];//过滤关键串的数组
        string fliterFlagString = "";
        bool[] filterPass = new bool[8]; //判断某一位是否需要跳过，不判断
        bool isFilter = true;  //成功过滤
        bool isError = false;   //过滤器构造不成功，有问题，无法过滤

        public bool IsFilter
        {
            get { return isFilter; }
        }
        public bool IsErr
        {
            get { return isError; }
        }
        public byte[] FilterFlagBytes
        {
            get { return filterFlagBytes; }
        }
        public string FliterFlagString
        {
            get { return fliterFlagString; }
        }
        public bool[] FilterPass
        {
            get { return filterPass; }
        }
        public CANFilter()
        {
            isFilter = true;
            for (int i = 0; i < filterFlagBytes.Length; i++)
            {
                filterFlagBytes[i] = (byte)255;
                filterPass[i] = false;
            }
        }
        //无“*”跳过，纯byte过滤的过滤器
        public CANFilter(byte[] filterBytes)
        {
            if (filterBytes.Length >= 8) //过滤串超过8个时，只截取前8个byte
            {
                for (int i = 0; i < filterFlagBytes.Length; i++)
                {
                    filterFlagBytes[i] = filterBytes[i];
                    filterPass[i] = false;
                }
            }
            else //当过滤串小于8个时，跳过超过过滤串长度的数据
            {
                if (filterBytes.Length % 2 == 0)
                {
                    for (int i = 0; i < filterBytes.Length; i++)
                    {
                        filterFlagBytes[i] = filterBytes[i];
                        filterPass[i] = false;
                    }
                    for (int i = filterBytes.Length; i < filterFlagBytes.Length; i++)
                    {
                        filterFlagBytes[i] = (byte)255;
                        filterPass[i] = true;
                    }
                }
                else
                {
                    isError = true;
                }

            }

        }
        //含“*”跳过过滤的过滤器，需先trim再使用
        public CANFilter(string filterString)
        {
            filterString = filterString.Trim();
            if (filterString.Length > 16)
            {
                fliterFlagString = filterString.Substring(0, 16);
            }
            else if (filterString.Length % 2 == 0)
            {
                fliterFlagString = filterString;
                for (int i = 0; i < (fliterFlagString.Length / 2); i++)
                {
                    filterPass[i] = false;
                }
                for (int i = (fliterFlagString.Length / 2); i < 8; i++)
                {
                    fliterFlagString += "FF";
                    filterPass[i] = true;

                }
            }
            else
            {
                isError = true;
            }
            //判断是否含有“*”
            if (filterString.Contains("*"))
            {
                for (int i = 0; i < filterString.Length / 2; i++)
                {
                    if (filterString.Substring(i * 2, 1) == "*")
                    {
                        filterPass[i] = true;//存在*，跳过该位置数据
                        string newfliterFlagString = fliterFlagString.Replace("**", "FF");//将*替换为F
                        fliterFlagString = newfliterFlagString;
                    }
                }
            }
            //仍然转化为bytes数组进行判断
            filterFlagBytes = StringToBytes(fliterFlagString);

        }

        public bool Filter(byte[] filterData)
        {
            if (filterData.Length % 2 != 0)
            {
                return false;
            }
            if (isError)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    if (filterPass[i])
                    {
                        continue;
                    }
                    if (filterData[i] != filterFlagBytes[i])
                    {
                        isFilter = false;
                        break;
                    }
                }
            }

            return isFilter;
        }

        /// <summary>
        /// 字符串转字节数组
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public byte[] StringToBytes(string input)
        {
            int len = input.Length;
            if (len % 2 != 0)
            {
                throw new Exception("输入的字符串长度有误，必须是偶数。");
            }
            byte[] bytes = new byte[len / 2];
            for (int i = 0; i < len / 2; i++)
            {
                if (!byte.TryParse(input.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber, null, out bytes[i]))
                {
                    throw new Exception(string.Format("在位置{0}处的字符无法转换为16进制字节", i * 2 + 1));
                }
            }
            return bytes;
        }

        /// <summary>
        /// 字节数组转字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string BytesToString(byte[] input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in input)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}