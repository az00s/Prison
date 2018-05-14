using Prison.App.Business.DependencyResolution;
using Prison.App.Common.DependencyResolution;
using Prison.App.Data.DependencyResolution;
using StructureMap.Configuration.DSL;

namespace Prison.App.Dependency
{
    public class PrisonAppDependencyRegistry : Registry
    {
        public PrisonAppDependencyRegistry()
        {

            Scan(s =>
            {
                
                s.AssemblyContainingType<PrisonAppCommonRegistry>();
                s.AssemblyContainingType<PrisonAppDataRegistry>();
                s.AssemblyContainingType<PrisonAppBusinessRegistry>();
                //s.WithDefaultConventions();
                s.LookForRegistries();

            });
        }

    }
}
