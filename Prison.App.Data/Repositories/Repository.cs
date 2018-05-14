using Prison.App.Common.Entities;
using Prison.App.Data.Interfaces;
using Prison.App.Data.Repositories;
using Prison.App.Data.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Prison.App.Data
{
    public class Repository : IRepository
    {
        //config file name
        private const string FILE_NAME = "app.config";

        private const string CONNECTION_NAME = "PrisonConnection";

        private string _connection;

        public Repository()
        {
            InitializeRepository();
        }

        private void InitializeRepository()
        {
            //get the full absolute path of config file
            string absolutePath = Path.Combine
                (
                AppDomain.CurrentDomain.SetupInformation.PrivateBinPath,
                FILE_NAME
                );

            //build configuration object 
            Configuration conf = ConfigurationManager.OpenMappedExeConfiguration(
                new ExeConfigurationFileMap { ExeConfigFilename = absolutePath }, ConfigurationUserLevel.None);

            //get the connection string from section of config file
            _connection = conf.ConnectionStrings.ConnectionStrings[CONNECTION_NAME].ConnectionString;

             
        }

        
    
        public IDataCommonOperation<Detainee> Detainees { get { return new DetaineeRepository(_connection); } }

        public ICollection<Detention> Detentions { get; set; } 

        public IDataCommonOperation<Employee> Employees { get { return new EmployeeRepository(_connection); } }

        public IDataCommonOperation<PlaceOfStay> PlacesOfStay { get { return new PlaceOfStayRepository(_connection); } }

       

        public void ErrorMethod()
        {
            throw new NullReferenceException();
        }
    }
}
