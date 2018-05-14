using Prison.App.Common.Interfaces;
using Prison.App.Common.Loggers;
using StructureMap.Configuration.DSL;

namespace Prison.App.Common.DependencyResolution
{
    public class PrisonAppCommonRegistry:Registry
    {
        public PrisonAppCommonRegistry()
        {

            ForSingletonOf<ILogger>().Use<Logger>();
            
        }
    }
}
