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
                (AppDomain.CurrentDomain.SetupInformation.PrivateBinPath,FILE_NAME);

            //set config file for using it in configuration object
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = absolutePath;

            //get configuration object for using it in ConfigurationChannelFactory
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            //get service model section from config
            var serviceModel = ServiceModelSectionGroup.GetSectionGroup(configuration);
            //get bindingConfiguration name of first end point
            string endpointConfigurationName = serviceModel.Client.Endpoints[0].BindingConfiguration;

            //------------------------------------------------------------------------------------------------
            ////get collection of end points from the config file
            //ChannelEndpointElementCollection endpointCollection = (ChannelEndpointElementCollection)configuration.SectionGroups["system.serviceModel"].Sections["client"].ElementInformation.Properties[""].Value;
            ////get bindingConfiguration name of first end point
            //string endpointConfigurationName = endpointCollection[0].BindingConfiguration;
            //-------------------------------------------------------------------------------------------------


            //build new factory using configuration object
            ConfigurationChannelFactory<IAdContract> ChannelFactory = new ConfigurationChannelFactory<IAdContract>(endpointConfigurationName, configuration, null);

            //create channel and initialize field
            _adService = ChannelFactory.CreateChannel();

        }

        public IEnumerable<IBlurb> GetElementsFromRep(int numOfElements)
        {
            List<Common.Entities.Blurb> listOfBlurbsOnClient = new List<Common.Entities.Blurb>();

            try
            {
                ((IClientChannel)_adService).Open();

                ServiceReference.Blurb[] listOfBlurbsFromService = _adService.GetRandomElementsFromRep(numOfElements);


                foreach (ServiceReference.Blurb blrb in listOfBlurbsFromService)
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
            catch (FaultException<ArgumentNullException> ex)
            {
                ((IClientChannel)_adService).Abort();
                _log.Error(ex.Detail.Message);
                listOfBlurbsOnClient = null;
            }

            catch (FaultException ex)
            {
                ((IClientChannel)_adService).Abort();
                _log.Error(ex.Message);
                listOfBlurbsOnClient = null;

            }
           
            catch (EndpointNotFoundException ex)
            {
                ((IClientChannel)_adService).Abort();
                _log.Error(ex.Message);
                listOfBlurbsOnClient = null;
            }

            catch (TimeoutException ex)
            {
                ((IClientChannel)_adService).Abort();
                _log.Error(ex.Message);
                listOfBlurbsOnClient = null;
            }

            catch (CommunicationException ex)
            {
                ((IClientChannel)_adService).Abort();
                _log.Error(ex.Message);
                listOfBlurbsOnClient = null;
            }

            finally
            {
                ((IClientChannel)_adService).Close();
            }

            return listOfBlurbsOnClient;
        }
    }
}
