using Prison.App.Business.Registers;
using Prison.App.Common.Registers;
using Prison.App.Data.Registers;
using StructureMap.Configuration.DSL;

namespace Prison.App.Dependency
{
    public class DependencyRegistry : Registry
    {
        public DependencyRegistry()
        {

            Scan(s =>
            {
                
                s.AssemblyContainingType<CommonRegistry>();
                s.AssemblyContainingType<DataRegistry>();
                s.AssemblyContainingType<BusinessRegistry>();
                s.LookForRegistries();

            });
        }

    }
}
