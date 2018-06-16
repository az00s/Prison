﻿using Prison.App.Business.Services;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
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

        public IReadOnlyCollection<PlaceOfStay> GetAllPlaces()
        {            
            var  result = _rep.GetAllPlaces();

            if (result == null)
            {
                throw new NullReferenceException("Не удалось получить список место содержания!");
            }

            return result;
        }

        public PlaceOfStay GetPlaceByID(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var result = _rep.GetPlaceByID(id);

                if (result == null)
                {
                    throw new NullReferenceException($"Место содержания с идентификатором: {id} не найдено!");
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
