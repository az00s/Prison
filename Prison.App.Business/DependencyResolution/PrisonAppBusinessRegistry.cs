using Prison.App.Business.Interfaces;
using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Data.ServiceReference;
using StructureMap.Configuration.DSL;

namespace Prison.App.Business.DependencyResolution
{
   public  class PrisonAppBusinessRegistry:Registry
    {
        public PrisonAppBusinessRegistry()
        {
            For<IAdvertismentProvider>().Use<AdvertismentProvider>();
            For<IDataProvider>().Use<DataProvider>();
            For<IBlurb>().Use<Blurb>();
        }
    }
}
