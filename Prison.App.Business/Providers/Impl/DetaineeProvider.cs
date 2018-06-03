using Prison.App.Business.Services;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Prison.App.Business.Providers.Impl
{
    public class DetaineeProvider: IDetaineeProvider
    {
        private ILogger _log;

        private IDetaineeRepository _rep;

        private ICachingService _cacheService;


        public DetaineeProvider(ILogger log, IDetaineeRepository rep,ICachingService cacheService)
        {
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");
            ArgumentHelper.ThrowExceptionIfNull(rep, "IDetaineeRepository");
            ArgumentHelper.ThrowExceptionIfNull(cacheService, "ICachingService");
            _log = log;
            _rep = rep;
            _cacheService = cacheService;
        }

        public IEnumerable<Detainee> GetAllRecordsFromTable()
        {
            IEnumerable<Detainee> result;

            if (_cacheService.Contains("AllDetaineeList"))
            {
                //get data from cache
                result = _cacheService.Get<IEnumerable<Detainee>>("AllDetaineeList");
            }

            else
            {
                //get data from dataBase if cache hasn't this data
                result = _rep.GetAllRecordsFromTable();

                if (result == null)
                {
                    throw new NullReferenceException("Не удалось получить список задержанных!");
                }

                //put data into cache
                else _cacheService.Add("AllDetaineeList", result, 7);
            }

            return result;

        }

        public Detainee GetDetaineeByID(int id)
        {

            if (ArgumentHelper.IsValidID(id))
            {
                Detainee result;

                if (_cacheService.Contains($"Detainee{id}"))
                {
                    //get data from cache
                    result = _cacheService.Get<Detainee>($"Detainee{id}");
                }

                else
                {
                    //get data from dataBase if cache hasn't this data
                    result = _rep.GetDetaineeByID(id);

                    if (result == null)
                    {
                        throw new NullReferenceException($"Задержанный с идентификатором: {id} не найден!");
                    }

                    //put data into cache
                    else _cacheService.Add($"Detainee{id}", result, 300);
                }

                return result;
            }
            else
            {
                throw new ArgumentException($"Идентификатор задержанного указан неверно.Пожалуйста укажите значение от 0 до {int.MaxValue}");
            }
        }

        public IEnumerable<Detainee> GetDetaineesByDate(DateTime date)
        {
            if (ArgumentHelper.IsValidDate(date))
            {
                IEnumerable<Detainee> result;

                if (_cacheService.Contains($"DetaineesBy{date}"))
                {
                    //get data from cache
                    result = _cacheService.Get<IEnumerable<Detainee>>($"DetaineesBy{date}");
                }

                else
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

        public IEnumerable<MaritalStatus> GetAllMaritalStatusesFromTable()
        {
            IEnumerable<MaritalStatus> result;

            if (_cacheService.Contains("AllMaritalStatusList"))
            {
                //get data from cache
                result = _cacheService.Get<IEnumerable<MaritalStatus>>("AllMaritalStatusList");
            }

            else
            {
                //get data from dataBase if cache hasn't this data
                result = _rep.GetAllMaritalStatusesFromTable();

                if (result == null)
                {
                    throw new NullReferenceException($"Не удалось получить список семейных статусов!");
                }

                //because of Marital satauses are not changing all time or could be changed rarely, cache for it was setted to 15 min
                else _cacheService.Add("AllMaritalStatusList", result, 900);
            }

            return result;

        }
        public void Create(Detainee dtn)
        {
            _rep.Create(dtn);
        }

        public void Update(Detainee dtn)
        {
            _rep.Update(dtn);

            //get data from cache

            if (_cacheService.Contains($"Detainee{dtn.DetaineeID}"))
            {
                _cacheService.Update($"Detainee{dtn.DetaineeID}", dtn, 300);
            }

            else //put data into cache
                _cacheService.Add($"Detainee{dtn.DetaineeID}", dtn, 300);
        }

        public void Delete(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                _rep.Delete(id);

                _cacheService.Delete($"Detainee{id}");

            }
            else
            {
                throw new ArgumentException($"Идентификатор задержанного указан неверно.Пожалуйста укажите значение от 0 до {int.MaxValue}");
            }
        }

        public IEnumerable<Detainee> GetDetaineesByParams(string DetentionDate=null, string FirstName = null, string LastName = null, string MiddleName = null, string ResidenceAddress = null)
        {
            return _rep.GetDetaineesByParams(DetentionDate, FirstName, LastName, MiddleName, ResidenceAddress);
        }
    }
}
