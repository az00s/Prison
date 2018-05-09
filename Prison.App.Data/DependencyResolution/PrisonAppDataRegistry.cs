using Prison.App.Data.Interfaces;
using Prison.App.Data.Services;
using StructureMap.Configuration.DSL;

namespace Prison.App.Data.DependencyResolution
{
    public class PrisonAppDataRegistry : Registry
    {
        public PrisonAppDataRegistry()
        {

            For<IRepository>().Use<Repository>();
            For<IAdService>().Use<AdService>();

        }
        
    }
}
