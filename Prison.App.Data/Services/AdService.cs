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
            var absolutePath = Path.Combine
                (AppDomain.CurrentDomain.SetupInformation.PrivateBinPath,FILE_NAME);

            //set config file for using it in configuration object
            var fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = absolutePath
            };

            try
            {
                //get configuration object for using it in ConfigurationChannelFactory
                var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

                //get service model section from config
                var serviceModel = ServiceModelSectionGroup.GetSectionGroup(configuration);

                //get bindingConfiguration name of first end point
                var endpointConfigurationName = serviceModel.Client.Endpoints[0].BindingConfiguration;

                //build new factory using configuration object
                var ChannelFactory = new ConfigurationChannelFactory<IAdContract>(endpointConfigurationName, configuration, null);

                //create channel and initialize field
                _adService = ChannelFactory.CreateChannel();
            }
            catch (ConfigurationErrorsException ex)
            {
                _log.Error("Can't read config file or config section! Config file is corrupt or does not exist!",ex);
            }

            catch (Exception ex)
            {
                _log.Error("Can't initialize AdService!", ex);
            }
        }

        public IReadOnlyCollection<IBlurb> GetAds(int numOfElements)
        {
            List<Common.Entities.Blurb> listOfBlurbsOnClient=null;

            try
            {
                ((IClientChannel)_adService).Open();

                var listOfBlurbsFromService = _adService.GetAd(numOfElements);

                listOfBlurbsOnClient = new List<Common.Entities.Blurb>();

                foreach (var blrb in listOfBlurbsFromService)
                {
                    listOfBlurbsOnClient.Add(
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
                _log.Error(ex.Message, ex);
            }
           
            catch (EndpointNotFoundException ex)
            {
                _log.Error(ex.InnerException.Message,ex);
            }

            catch (TimeoutException ex)
            {
                _log.Error(ex.Message,ex);
            }

            catch (CommunicationException ex)
            {
                _log.Error(ex.Message, ex);
            }

            finally
            {
                ((IClientChannel)_adService).Close();
            }

            return listOfBlurbsOnClient;
        }
    }
}
