﻿using Prison.App.Business.Services;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Prison.App.Business.Providers.Impl
{
    public class DetaineeProvider: IDetaineeProvider
    {
        private IDetaineeRepository _rep;

        private ICachingService _cacheService;

        public DetaineeProvider(IDetaineeRepository rep,ICachingService cacheService)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IDetaineeRepository");
            ArgumentHelper.ThrowExceptionIfNull(cacheService, "ICachingService");
            _rep = rep;
            _cacheService = cacheService;
        }

        public IReadOnlyCollection<Detainee> GetAllDetainees()
        {           
            var  result = _cacheService.Get<IReadOnlyCollection<Detainee>>("AllDetaineeList");
            
            if(result==null)
            {
                //get data from dataBase if cache hasn't this data
                result = _rep.GetAllDetainees();

                if (result == null)
                {
                    throw new NullReferenceException("Не удалось получить список задержанных!");
                }

                //put data into cache
                else _cacheService.Add("AllDetaineeList", result, 10);
            }

            return result;
        }

        public Detainee GetDetaineeByID(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {                
                   var result = _rep.GetDetaineeByID(id);

                    if (result == null)
                    {
                        throw new NullReferenceException($"Задержанный с идентификатором: {id} не найден!");
                    }

                return result;
            }
            else
            {
                throw new ArgumentException($"Идентификатор задержанного указан неверно.Пожалуйста укажите значение от 0 до {int.MaxValue}");
            }
        }

        public Release GetLastRelease(int id)
        {
            return _rep.GetLastRelease(id);
        }

        public Release GetRelease(int detaineeID, int detentionID)
        {
            return _rep.GetRelease(detaineeID, detentionID);
        }

        public IReadOnlyCollection<Detainee> GetDetaineesByDate(DateTime date)
        {
            if (ArgumentHelper.IsValidDate(date))
            {
                    var result = _rep.GetDetaineesByDate(date);

                    if (result == null)
                    {
                        throw new NullReferenceException($"Задержанные на дату: {date} не найдены!");
                    }
                return result;

            }
            else
            {
                throw new ArgumentException($"Неверная дата {date}!");
            }
        }

        public IReadOnlyCollection<MaritalStatus> GetAllMaritalStatuses()
        {
                var result = _rep.GetAllMaritalStatuses();

                if (result == null)
                {
                    throw new NullReferenceException($"Не удалось получить список семейных статусов!");
                }

            return result;
        }

        public IReadOnlyCollection<Detainee> Find(string DetentionDate, string FirstName, string LastName, string MiddleName, string ResidenceAddress)
        {
            if (string.IsNullOrEmpty(DetentionDate) && string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName) && string.IsNullOrEmpty(MiddleName) && string.IsNullOrEmpty(ResidenceAddress))
            {
                return null;
            }

            return _rep.Find(DetentionDate, FirstName, LastName, MiddleName, ResidenceAddress);
        }
    }
}
