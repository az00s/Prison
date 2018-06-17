using System;
using System.Configuration;
using System.IO;

namespace Prison.App.Common.Helpers
{
    public class ConnectionStringHelper: IConnectionStringHelper
    {
        private string _connString;

        //config file name
        private const string FILE_NAME = "app.config";

        private const string CONNECTION_NAME = "PrisonConnection";
                               
        public string GetConnectionString()
        {
            if (_connString != null)
            {
                return _connString;
            }

            //get the full absolute path of config file
            string absolutePath = Path.Combine
                (AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, FILE_NAME);

            //build configuration object 
            Configuration conf = ConfigurationManager.OpenMappedExeConfiguration(
                new ExeConfigurationFileMap { ExeConfigFilename = absolutePath }, ConfigurationUserLevel.None);

            //get the connection string from section of config file
            _connString = conf.ConnectionStrings.ConnectionStrings[CONNECTION_NAME].ConnectionString;

            return _connString;
        }
    }
}
