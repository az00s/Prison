﻿using Prison.App.Common.Interfaces;
using Prison.App.Common.Loggers;
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
            For<IEmployeeRepository>().Use<EmployeeRepository>();
            For<IDetaineeRepository>().Use<DetaineeRepository>();
            For<IPlaceOfStayRepository>().Use<PlaceOfStayRepository>();
            For<IPositionRepository>().Use<PositionRepository>();
            For<IAdService>().Use<AdService>();
            ForSingletonOf<ILogger>().Use<Logger>();
        }
    }
}