using Prison.App.Business.Services;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Prison.App.Business.Providers.Impl
{
    public class PlaceProvider:IPlaceProvider
    {
        private IPlaceOfStayRepository _rep;

        private ICachingService _cacheService;

        public PlaceProvider(IPlaceOfStayRepository rep, ICachingService cacheService)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IPlaceOfStayRepository");
            ArgumentHelper.ThrowExceptionIfNull(cacheService, "ICachingService");

            _rep = rep;
            _cacheService = cacheService;
        }

        public IEnumerable<PlaceOfStay> GetAllPlaces()
        {            
            //get data from cache
            var  result = _cacheService.Get<IEnumerable<PlaceOfStay>>("AllPlaceOfStayList");

            if (result == null)
            {
                //get data from dataBase if cache hasn't this data
                result = _rep.GetAllPlaces();

                if (result == null)
                {
                    throw new NullReferenceException("Не удалось получить список место содержания!");
                }

                //put data into cache
                else _cacheService.Add("AllPlaceOfStayList", result, 7);
            }

            return result;

        }

        public PlaceOfStay GetPlaceByID(int id)
        {

            if (ArgumentHelper.IsValidID(id))
            {
                //get data from cache
                var result = _cacheService.Get<PlaceOfStay>($"PlaceOfStay{id}");

                if (result == null)
                {
                    //get data from dataBase if cache hasn't this data
                    result = _rep.GetPlaceByID(id);

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

    }
}
