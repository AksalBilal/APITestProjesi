using System;
using System.Reflection;
using log4net;
using NUnit.Framework;

namespace TechTestAPI.Helpers
{
    internal class Logger
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static Logger _instance;

        //private List<LoggerException> criticalLogList { get; set; }

        private Logger()
        {
        }

        public static Logger GetInstance()
        {
            return _instance ?? (_instance = new Logger());
        }

        public void ExceptionLog(TestContext currentResult)
        {
            Log.Error(Environment.NewLine + "Method Name   =>" + currentResult.Test.MethodName +
                      currentResult.Result.Message + Environment.NewLine +
                      "*******************************" + Environment.NewLine);
        }
    }
}