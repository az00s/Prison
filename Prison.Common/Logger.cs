using log4net;
using Prison.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.Common
{
    public class Logger:ILogger
    {
        ILog logger;

        public Logger()
        {
            logger = LogManager.GetLogger("RollingLogFileAppender");
        }



        public void Debug(string message)
        {
            logger.Debug(message);
        }


        public void Debug(string message, System.Exception exception)
        {
            logger.Debug(message, exception);
        }


        public  void Info(string message)
        {
            logger.Info(message);
        }


        public  void Info(string message, System.Exception exception)
        {
            logger.Info(message, exception);
        }

        public  void Warn(string message)
        {
            logger.Warn(message);
        }

        public  void Warn(string message, System.Exception exception)
        {
            logger.Warn(message, exception);
        }

        public  void Error(string message)
        {
            logger.Error(message);
        }

        public  void Error(string message, System.Exception exception)
        {
            logger.Error(message, exception);
        }


        public  void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public  void Fatal(string message, System.Exception exception)
        {
            logger.Fatal(message, exception);
        }


    }
}
