using Prison.App.Business.Interfaces;
using Prison.App.Business.Providers;
using Prison.App.Business.Providers.Impl;
using Prison.App.Common.Entities;
using StructureMap.Configuration.DSL;

namespace Prison.App.Business.Registers
{
    public  class BusinessRegistry:Registry
    {
        public BusinessRegistry()
        {
            For<IBlurb>().Use<Common.Entities.Blurb>();
            For<IAdvertismentProvider>().Use<AdvertismentProvider>();
            For<IEmployeeProvider>().Use<EmployeeProvider>();
            For<IDetaineeProvider>().Use<DetaineeProvider>();
            For<IPlaceOfStayProvider>().Use<PlaceOfStayProvider>();
            For<IPositionProvider>().Use<PositionProvider>();
        }
    }
}
