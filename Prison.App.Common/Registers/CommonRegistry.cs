using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Common.Loggers;
using StructureMap.Configuration.DSL;

namespace Prison.App.Common.Registers
{
    public class CommonRegistry:Registry
    {
        public CommonRegistry()
        {
            ForSingletonOf<ILogger>().Use<Logger>();
            For<IConnectionStringHelper>().Use<ConnectionStringHelper>();
        }
    }
}
