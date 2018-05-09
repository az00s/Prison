using Prison.App.Data.Interfaces;
using Prison.App.Data.ServiceReference;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Data.Services
{
    public class AdService: IAdService
    {
        const string FILE_NAME = "Service.config";

        IAdContract _adService;

        public AdService()
        {
            InitializeService();
        }

        void InitializeService()
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
        public IEnumerable<Blurb> GetAds()
        {
            return _adService.GetAds();
        }
    }
}
