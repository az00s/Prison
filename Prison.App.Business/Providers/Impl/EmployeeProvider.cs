using Prison.App.Business.Services;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Prison.App.Business.Providers.Impl
{
    public class EmployeeProvider:IEmployeeProvider
    {
        private ILogger _log;

        private IEmployeeRepository _rep;

        private ICachingService _cacheService;

        public EmployeeProvider(ILogger log,IEmployeeRepository rep, ICachingService cacheService)
        {
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");
            ArgumentHelper.ThrowExceptionIfNull(rep, "IEmployeeRepository");
            ArgumentHelper.ThrowExceptionIfNull(cacheService, "ICachingService");
            _log = log;
            _rep = rep;
            _cacheService = cacheService;
        }

        public IEnumerable<Employee> GetAllRecordsFromTable()
        {
            IEnumerable<Employee> result;

            if (_cacheService.Contains("AllEmployeeList"))
            {
                //get data from cache
                result = _cacheService.Get<IEnumerable<Employee>>("AllEmployeeList");
            }

            else
            {
                //get data from dataBase if cache hasn't this data
                result = _rep.GetAllRecordsFromTable();

                if (result == null)
                {
                    throw new NullReferenceException("Не удалось получить список сотрудников!");
                }

                //put data into cache
                else _cacheService.Add("AllEmployeeList", result, 7);
            }

            return result;
        }

        public Employee GetEmployeeByID(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                Employee result;

                if (_cacheService.Contains($"Employee{id}"))
                {
                    //get data from cache
                    result = _cacheService.Get<Employee>($"Employee{id}");
                }

                else
                {
                    //get data from dataBase if cache hasn't this data
                    result = _rep.GetEmployeeByID(id);

                    if (result == null)
                    {
                        throw new NullReferenceException($"Сотрудник с идентификатором: {id} не найден!");
                    }

                    //put data into cache
                    else _cacheService.Add($"Employee{id}", result, 300);
                }

                return result;
            }
            else
            {
                throw new ArgumentException($"Идентификатор сотрудника указан неверно.Пожалуйста укажите значение от 0 до {int.MaxValue}");
            }
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
