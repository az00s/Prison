using Prison.App.Common.Interfaces;
using Prison.App.Data.Interfaces;
using StructureMap.Configuration.DSL;

namespace Prison.App.Dependency
{
    public class PrisonRegistry:Registry
    {
        public PrisonRegistry()
        {

            Scan(s =>
            {
                s.AssemblyContainingType<ILogger>();
                s.AssemblyContainingType<IRepository>();
                s.WithDefaultConventions();
                s.LookForRegistries();
            });
        }

    }
}
