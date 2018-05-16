using Prison.App.Business.Interfaces;
using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Data.ServiceReference;
using StructureMap.Configuration.DSL;

namespace Prison.App.Business.Registers
{
   public  class BusinessRegistry:Registry
    {
        public BusinessRegistry()
        {
            For<IAdvertismentProvider>().Use<AdvertismentProvider>();
            For<IDataProvider>().Use<DataProvider>();
            For<IBlurb>().Use<Common.Entities.Blurb>();
        }
    }
}
