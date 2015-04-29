using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace MFS.Common
{
    /// <summary>
    /// 日志操作
    /// </summary>
    public class LogManagerHelper
    {
        readonly static ILog Log = log4net.LogManager.GetLogger("ETVSLogger");
        /// <summary>
        /// 初始化并启动日志
        /// </summary>
        public static void Init()
        {
            log4net.Config.XmlConfigurator.Configure(); //启动日志
        }
        public static void Error(object message, Exception exception)
        {
            Log.Error(message, exception);
        }
        public static void Info(object message)
        {
            Log.Info(message);
        }
    }
}
