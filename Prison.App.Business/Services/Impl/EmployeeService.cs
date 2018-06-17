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

            //data changed - list invalid
            _cacheService.Delete("AllEmployeeList");
        }

        public void Update(Employee emp)
        {
            _rep.Update(emp);

            //data changed - list invalid
            _cacheService.Delete("AllEmployeeList");
        }

        public void Delete(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                _rep.Delete(id);

                //data changed - list invalid
                _cacheService.Delete("AllEmployeeList");
            }
            else
            {
                throw new ArgumentException($"Идентификатор сотрудника указан неверно.Пожалуйста укажите значение от 0 до {int.MaxValue}");
            }
        }
    }
}
