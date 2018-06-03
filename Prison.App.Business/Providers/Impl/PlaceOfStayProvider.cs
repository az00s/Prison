using Prison.App.Business.Services;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Prison.App.Business.Providers.Impl
{
    public class PlaceOfStayProvider:IPlaceOfStayProvider
    {
        private ILogger _log;

        private IPlaceOfStayRepository _rep;

        private ICachingService _cacheService;

        public PlaceOfStayProvider(ILogger log, IPlaceOfStayRepository rep, ICachingService cacheService)
        {
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");
            ArgumentHelper.ThrowExceptionIfNull(rep, "IPlaceOfStayRepository");
            ArgumentHelper.ThrowExceptionIfNull(cacheService, "ICachingService");

            _log = log;
            _rep = rep;
            _cacheService = cacheService;
        }

        public IEnumerable<PlaceOfStay> GetAllRecordsFromTable()
        {

            IEnumerable<PlaceOfStay> result;

            if (_cacheService.Contains("AllPlaceOfStayList"))
            {
                //get data from cache
                result = _cacheService.Get<IEnumerable<PlaceOfStay>>("AllPlaceOfStayList");
            }

            else
            {
                //get data from dataBase if cache hasn't this data
                result = _rep.GetAllRecordsFromTable();

                if (result == null)
                {
                    throw new NullReferenceException("Не удалось получить список место содержания!");
                }

                //put data into cache
                else _cacheService.Add("AllPlaceOfStayList", result, 7);
            }

            return result;

        }

        public PlaceOfStay GetPlaceOfStayByID(int id)
        {

            if (ArgumentHelper.IsValidID(id))
            {
                PlaceOfStay result;

                if (_cacheService.Contains($"PlaceOfStay{id}"))
                {
                    //get data from cache
                    result = _cacheService.Get<PlaceOfStay>($"PlaceOfStay{id}");
                }

                else
                {
                    //get data from dataBase if cache hasn't this data
                    result = _rep.GetPlaceOfStayByID(id);

                    if (result == null)
                    {
                        throw new NullReferenceException($"Место содержания с идентификатором: {id} не найдено!");
                    }

                    //put data into cache
                    else _cacheService.Add($"PlaceOfStay{id}", result, 300);
                }

                return result;
            }
            else
            {
                throw new ArgumentException($"Идентификатор Места содержания указан неверно.Пожалуйста укажите значение от 0 до {int.MaxValue}");
            }
            
        }

        public void Create(PlaceOfStay plc)
        {
            _rep.Create(plc);
        }

        public void Update(PlaceOfStay plc)
        {
            _rep.Update(plc);

            if (_cacheService.Contains($"PlaceOfStay{plc.PlaceID}"))
            {
                _cacheService.Update($"PlaceOfStay{plc.PlaceID}", plc, 300);
            }

            else //put data into cache
                _cacheService.Add($"PlaceOfStay{plc.PlaceID}", plc, 300);
        }

        public void Delete(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                _rep.Delete(id);

                _cacheService.Delete($"PlaceOfStay{id}");

            }
            else
            {
                throw new ArgumentException($"Идентификатор Места содержания указан неверно.Пожалуйста укажите значение от 0 до {int.MaxValue}");
            }
        }
    }
}
