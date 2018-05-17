using Prison.App.Common.Interfaces;
using Prison.App.Common.Loggers;
using Prison.App.Data.Interfaces;
using Prison.App.Data.Services;
using StructureMap.Configuration.DSL;

namespace Prison.App.Data.Registers
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            For<IRepository>().Use<Repository>();
            For<IAdService>().Use<AdService>();
            ForSingletonOf<ILogger>().Use<Logger>();
        }
    }
}
