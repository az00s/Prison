using Prison.App.Business.Services;
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

        public IEnumerable<Detainee> GetAllDetainees()
        {           
            var  result = _cacheService.Get<IEnumerable<Detainee>>("AllDetaineeList");
            
            if(result==null)
            {
                //get data from dataBase if cache hasn't this data
                result = _rep.GetAllDetainees();

                if (result == null)
                {
                    throw new NullReferenceException("Не удалось получить список задержанных!");
                }

                //put data into cache
                else _cacheService.Add("AllDetaineeList", result, 7);
            }

            return result;

        }

        public IEnumerable<Detention> GetAllDetentions()
        {
            return _rep.GetAllDetentions();
        }
        public Detainee GetDetaineeByID(int id)
        {

            if (ArgumentHelper.IsValidID(id))
            {
                //get data from cache
                var  result = _cacheService.Get<Detainee>($"Detainee{id}");
                

                if(result==null)
                {
                    //get data from dataBase if cache hasn't this data
                    result = _rep.GetDetaineeByID(id);

                    if (result == null)
                    {
                        throw new NullReferenceException($"Задержанный с идентификатором: {id} не найден!");
                    }

                    //put data into cache
                    else _cacheService.Add($"Detainee{id}", result, 60);
                }

                return result;
            }
            else
            {
                throw new ArgumentException($"Идентификатор задержанного указан неверно.Пожалуйста укажите значение от 0 до {int.MaxValue}");
            }
        }

        public Detention GetDetentionByID(int id)
        {
            return _rep.GetDetentionByID(id);
        }
        public Detention GetLastDetention(int id)
        {
            return _rep.GetLastDetention(id);
        }

        public IEnumerable<Detainee> GetDetaineesByDate(DateTime date)
        {
            if (ArgumentHelper.IsValidDate(date))
            {
                //get data from cache
                var result = _cacheService.Get<IEnumerable<Detainee>>($"DetaineesBy{date}");

                if(result==null)
                {
                    //get data from dataBase if cache hasn't this data
                    result = _rep.GetDetaineesByDate(date);

                    if (result == null)
                    {
                        throw new NullReferenceException($"Задержанные на дату: {date} не найдены!");
                    }

                    //put data into cache
                    else _cacheService.Add($"DetaineesBy{date}", result, 15);
                }

                return result;

            }
            else
            {
                throw new ArgumentException($"Неверная дата {date}!");
            }
        }

        public IEnumerable<MaritalStatus> GetAllMaritalStatuses()
        {
            //get data from cache
            var result = _cacheService.Get<IEnumerable<MaritalStatus>>("AllMaritalStatusList");

            if(result==null)
            {
                //get data from dataBase if cache hasn't this data
                result = _rep.GetAllMaritalStatuses();

                if (result == null)
                {
                    throw new NullReferenceException($"Не удалось получить список семейных статусов!");
                }

                //because of Marital satauses are not changing all time or could be changed rarely, cache for it was setted to 5 min
                else _cacheService.Add("AllMaritalStatusList", result, 300);
            }

            return result;

        }

        public IEnumerable<Detainee> GetDetaineesByParams(string DetentionDate=null, string FirstName = null, string LastName = null, string MiddleName = null, string ResidenceAddress = null)
        {
            return _rep.GetDetaineesByParams(DetentionDate, FirstName, LastName, MiddleName, ResidenceAddress);
        }
    }
}
