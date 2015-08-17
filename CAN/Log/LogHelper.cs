using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace TTOperator.Util.Log
{
    public class LogHelper
    {
        #region FATAL级别
        /// <summary>
        /// 输出Fatal日志到LOG4NET
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void WriteFatalLog(Type t, Exception ex)
        public static void WriteFatalLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Fatal(ex.Message, ex);
        }
        #endregion
        /// <summary>
        /// 输出Fatal日志到LOG4NET
        /// </summary>
        /// <param name="msg"></param>
        #region static void WriteFatalLog(string msg)
        public static void WriteFatalLog(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("FATAL");
            log.Fatal(msg);
        }
        #endregion


        #endregion

        #region Error级别
        /// <summary>
        /// 输出Error日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void WriteErrorLog(Type t, Exception ex)

        public static void WriteErrorLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        #endregion
        /// <summary>
        /// 输出Error日志到Log4Net
        /// </summary>
        /// <param name="msg"></param>
        #region static void WriteErrorLog(string msg)

        public static void WriteErrorLog(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("ERROR");
            log.Error(msg);
        }

        #endregion
        #endregion

        #region Warn
        /// <summary>
        /// 输出Warn日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void WriteWarnLog(Type t, Exception ex)

        public static void WriteWarnLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Warn("Error", ex);
        }

        #endregion
        /// <summary>
        /// 输出Warn日志到Log4Net
        /// </summary>
        /// <param name="msg"></param>
        #region static void WriteWarnLog(string msg)

        public static void WriteWarnLog(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("ERROR");
            log.Warn(msg);
        }

        #endregion
        #endregion

        #region INFO
        /// <summary>
        /// 输出INFO日志到LOG4NET
        /// </summary>
        /// <param name="msg"></param>
        #region static void WriteInfoLog(string info)
        public static void WriteInfoLog(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("INFO");
            log.Info(msg);
        }
        #endregion
        #endregion

        #region DEBUG
        /// <summary>
        /// 输出DEBUG日志到LOG4NET
        /// </summary>
        /// <param name="msg"></param>
        #region static void WriteDebugLog(string msg)
        public static void WriteDebugLog(string msg)
        {
            log4net.ILog Log = log4net.LogManager.GetLogger("DEBUG");
            Log.Debug(msg);
        }
        #endregion

        /// <summary>
        /// 输出DEBUG日志到LOG4NET
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        #region static void WriteDebugLog(string msg, Exception ex)
        public static void WriteDebugLog(string msg, Exception ex)
        {
            log4net.ILog Log = log4net.LogManager.GetLogger("DEBUG");
            Log.Debug(msg, ex);
        }

        #endregion

        /// <summary>
        /// 输出DEBUG日志到LOG4NET
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void WriteDebugLog(Type t, string msg)
        public static void WriteDebugLog(Type t, string msg)
        {
            log4net.ILog Log = log4net.LogManager.GetLogger(t);
            Log.Debug(msg);
        }
        #endregion


        #endregion


    }
}
