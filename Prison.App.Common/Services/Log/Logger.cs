using log4net;
using log4net.Config;
using Prison.App.Common.Interfaces;
using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace Prison.App.Common.Loggers
{
    public class Logger:ILogger
    {
        private const string FILE_NAME = "Log.config";
        private const string LOGGER_NAME = "PrisonLogger";


        private  readonly ILog logger;

        public Logger()
        {
            ConfigureLogger();
            logger = LogManager.GetLogger(LOGGER_NAME);
            
        }

        private void ConfigureLogger()
        {
            //get base directory
            string baseDirectory = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
            //get the path of config file
            string filePath = Path.Combine(baseDirectory, FILE_NAME);
            //get FileInfo
            FileInfo fileInfo = new FileInfo(filePath);

            if (fileInfo.Exists)
            {
                //configure logger
                XmlConfigurator.ConfigureAndWatch(fileInfo);
            }
            else
            {
                throw new FileNotFoundException(

                    String.Format("Logging configuration file {0} on path: {1} not found!", FILE_NAME,filePath)

                                                );
            }
            

            
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
