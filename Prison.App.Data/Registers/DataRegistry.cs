using Prison.App.Common.Interfaces;
using Prison.App.Common.Loggers;
using Prison.App.Data.DataContext;
using Prison.App.Data.DataContext.Impl;
using Prison.App.Data.Interfaces;
using Prison.App.Data.Repositories;
using Prison.App.Data.Repositories.Impl;
using Prison.App.Data.Services;
using StructureMap.Configuration.DSL;

namespace Prison.App.Data.Registers
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            //services
            ForSingletonOf<ILogger>().Use<Logger>();
            For<IAdService>().Use<AdService>();

            //Repositories
            For<IEmployeeRepository>().Use<EmployeeRepository>();
            For<IDetaineeRepository>().Use<DetaineeRepository>();
            For<IPlaceOfStayRepository>().Use<PlaceOfStayRepository>();
            For<IPositionRepository>().Use<PositionRepository>();
            For<IRoleRepository>().Use<RoleRepository>();
            For<IUserRepository>().Use<UserRepository>();
            For<IStatusRepository>().Use<StatusRepository>();

            //dataContext
            For(typeof(IDataContext<>)).Use(typeof(DataContext<>));
            For<IDetaineeDataContext>().Use<DetaineeDataContext>();
            For<IEmployeeDataContext>().Use<EmployeeDataContext>();
            For<IPlaceDataContext>().Use<PlaceDataContext>();
            For<IPositionDataContext>().Use<PositionDataContext>();
            For<IUserDataContext>().Use<UserDataContext>();
            For<IRoleDataContext>().Use<RoleDataContext>();
            For<IMaritalStatusDataContext>().Use<MaritalStatusDataContext>();
        }
    }
}
