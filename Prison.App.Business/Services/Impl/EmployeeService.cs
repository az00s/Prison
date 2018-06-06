using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Repositories;
using System;

namespace Prison.App.Business.Services.Impl
{
    public class EmployeeService: IEmployeeService
    {
        private IEmployeeRepository _rep;

        private ICachingService _cacheService;

        public EmployeeService(IEmployeeRepository rep, ICachingService cacheService)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IEmployeeRepository");
            ArgumentHelper.ThrowExceptionIfNull(cacheService, "ICachingService");
            _rep = rep;
            _cacheService = cacheService;
        }


        public void Create(Employee emp)
        {
            _rep.Create(emp);
        }

        public void Update(Employee emp)
        {
            _rep.Update(emp);

            //get data from cache

            if (_cacheService.Contains($"Employee{emp.EmployeeID}"))
            {
                _cacheService.Update($"Employee{emp.EmployeeID}", emp, 300);
            }

            else //put data into cache
                _cacheService.Add($"Employee{emp.EmployeeID}", emp, 300);
        }

        public void Delete(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                _rep.Delete(id);

                _cacheService.Delete($"Employee{id}");

            }
            else
            {
                throw new ArgumentException($"Идентификатор сотрудника указан неверно.Пожалуйста укажите значение от 0 до {int.MaxValue}");
            }
        }

    }
}
