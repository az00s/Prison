﻿using Prison.App.Business.Interfaces;
using Prison.App.Business.Providers;
using Prison.App.Business.Providers.Impl;
using Prison.App.Business.Services;
using Prison.App.Business.Services.Impl;
using Prison.App.Common.Entities;
using StructureMap.Configuration.DSL;

namespace Prison.App.Business.Registers
{
    public  class BusinessRegistry:Registry
    {
        public BusinessRegistry()
        {
            For<IBlurb>().Use<Common.Entities.Blurb>();

            For<ILogInService>().Use<LogInService>();
            ForSingletonOf<ICachingService>().Use<СachingService>();

            For<IAdvertismentProvider>().Use<AdvertismentProvider>();
            For<IEmployeeProvider>().Use<EmployeeProvider>();
            For<IDetaineeProvider>().Use<DetaineeProvider>();
            For<IPlaceProvider>().Use<PlaceProvider>();
            For<IPositionProvider>().Use<PositionProvider>();
            For<IUserProvider>().Use<UserProvider>();
            For<IRoleProvider>().Use<RoleProvider>();
            For<IStatusProvider>().Use<StatusProvider>();
            For<IPhoneNumberProvider>().Use<PhoneNumberProvider>();

            For<IEmployeeService>().Use<EmployeeService>();
            For<IDetaineeService>().Use<DetaineeService>();
            For<IPlaceService>().Use<PlaceService>();
            For<IUserService>().Use<UserService>();
            For<IRoleService>().Use<RoleService>();
            For<IPositionService>().Use<PositionService>();
            For<IStatusService>().Use<StatusService>();
            For<IPhoneNumberService>().Use<PhoneNumberService>();


        }
    }
}
