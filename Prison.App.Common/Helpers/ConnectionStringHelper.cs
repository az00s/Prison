using Prison.App.Common.Interfaces;
using StructureMap.Attributes;
using System;
using System.Configuration;
using System.IO;

namespace Prison.App.Common.Helpers
{
    public static class ConnectionStringHelper
    {
        private static string _connString=null;

        //config file name
        private const string FILE_NAME = "app.config";

        private const string CONNECTION_NAME = "PrisonConnection";
                               
        public static string GetConnectionString()
        {
            if (_connString != null) return _connString;
            try
            {
                //get the full absolute path of config file
                string absolutePath = Path.Combine
                    (AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, FILE_NAME);

                //build configuration object 
                Configuration conf = ConfigurationManager.OpenMappedExeConfiguration(
                    new ExeConfigurationFileMap { ExeConfigFilename = absolutePath }, ConfigurationUserLevel.None);

                //get the connection string from section of config file
                _connString = conf.ConnectionStrings.ConnectionStrings[CONNECTION_NAME].ConnectionString;
            }
            catch (NullReferenceException)
            {
                _connString = null;
            }
            catch (ArgumentException)
            {
                _connString = null;
            }
            return _connString;
        }
    }
}
