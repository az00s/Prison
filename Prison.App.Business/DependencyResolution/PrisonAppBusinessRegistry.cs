using Prison.App.Business.Interfaces;
using Prison.App.Business.Providers;
using StructureMap.Configuration.DSL;

namespace Prison.App.Business.DependencyResolution
{
   public  class PrisonAppBusinessRegistry:Registry
    {
        public PrisonAppBusinessRegistry()
        {
            For<IAdvertismentProvider>().Use<AdvertismentProvider>();
        }
    }
}
