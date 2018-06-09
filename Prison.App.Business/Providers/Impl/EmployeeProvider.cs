﻿using Prison.App.Business.Services;
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
        private IEmployeeRepository _rep;

        private ICachingService _cacheService;

        public EmployeeProvider(IEmployeeRepository rep, ICachingService cacheService)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IEmployeeRepository");
            ArgumentHelper.ThrowExceptionIfNull(cacheService, "ICachingService");
            _rep = rep;
            _cacheService = cacheService;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            //get data from cache
            var  result = _cacheService.Get<IEnumerable<Employee>>("AllEmployeeList");

            if(result==null)
            {
                //get data from dataBase if cache hasn't this data
                result = _rep.GetAllEmployees();

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
                    //get data from cache
                var result = _cacheService.Get<Employee>($"Employee{id}");

                if (result == null)
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

    }
}
