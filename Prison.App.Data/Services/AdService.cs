using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Interfaces;
using Prison.App.Data.ServiceReference;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Data.Services
{
    public class AdService: IAdService
    {
        const string FILE_NAME = "app.config";

        ILogger _log;

        IAdContract _adService;

        public AdService(ILogger log)
        {
            ArgumentHelper.ThrowExceptionIfNull(log,"ILogger");
            _log = log;

            InitializeAdService();
        }

        void InitializeAdService()
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
        public IEnumerable<Blurb> GetRandomElementsFromRep(int numOfElements)
        {
            IEnumerable<Blurb> listOfBlurbs;
            try
            {
                //throw new FaultException();
                listOfBlurbs = _adService.GetRandomElementsFromRep(numOfElements);
            }

            catch (FaultException ex)
            {
                //log the error
                _log.Error(ex.Message);

                listOfBlurbs = new List<Blurb> {
                    new Blurb { BlurbContent = "Мы против рекламы" }
                };
            }
            return listOfBlurbs;
        }
    }
}
