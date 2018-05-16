using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Interfaces;
using Prison.App.Data.ServiceReference;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Configuration;

namespace Prison.App.Data.Services
{
    public class AdService: IAdService
    {
        private const string FILE_NAME = "app.config";

        private ILogger _log;

        private IAdContract _adService;

        public AdService(ILogger log)
        {
            ArgumentHelper.ThrowExceptionIfNull(log,"ILogger");
            _log = log;

            InitializeAdService();
        }

        private void InitializeAdService()
        {
            //get the full absolute path of config file
            string absolutePath = Path.Combine
                (
                AppDomain.CurrentDomain.SetupInformation.PrivateBinPath,
                FILE_NAME
                );
            //get configuration object for using it in ConfigurationChannelFactory
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration
                (
                new ExeConfigurationFileMap { ExeConfigFilename = absolutePath },
                ConfigurationUserLevel.None
                );

            //build new factory using configuration object
            ConfigurationChannelFactory<IAdContract> ChannelFactory = new ConfigurationChannelFactory<IAdContract>("BasicHttpBinding_IAdContract", configuration, null);

            //create channel and initialize field
            _adService = ChannelFactory.CreateChannel();

        }

        public IEnumerable<IBlurb> GetRandomElementsFromRep(int numOfElements)
        {
            List<Common.Entities.Blurb> listOfBlurbs = new List<Common.Entities.Blurb>();
            try
            {
                //throw new FaultException();
                ServiceReference.Blurb[] listOfBlurbsFromService = _adService.GetRandomElementsFromRep(numOfElements);

                foreach (ServiceReference.Blurb blrb in listOfBlurbsFromService)
                {
                    listOfBlurbs.Add(
                        new Common.Entities.Blurb()
                        {
                            BlurbID = blrb.BlurbID,
                            BlurbHeader = blrb.BlurbHeader,
                            BlurbContent = blrb.BlurbContent,
                            Image = blrb.Image
                        });
                }

            }

            catch (FaultException ex)
            {
                //log the error
                _log.Error(ex.Message);

                listOfBlurbs = new List<Common.Entities.Blurb> {
                    new Common.Entities.Blurb { BlurbContent = "Мы против рекламы" }
                };
            }
            catch (EndpointNotFoundException ex)
            {
                _log.Error(ex.Message);

                listOfBlurbs = new List<Common.Entities.Blurb> {
                    new Common.Entities.Blurb { BlurbContent = "Мы против рекламы" }
                };
            }
            return listOfBlurbs;
        }
    }
}
