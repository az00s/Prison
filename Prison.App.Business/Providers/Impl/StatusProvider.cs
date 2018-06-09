using Prison.App.Business.Services;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Prison.App.Business.Providers.Impl
{
    class StatusProvider:IStatusProvider
    {
        private IStatusRepository _rep;

        private ICachingService _cacheService;

        public StatusProvider(IStatusRepository rep, ICachingService cacheService)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IStatusRepository");
            ArgumentHelper.ThrowExceptionIfNull(cacheService, "ICachingService");

            _rep = rep;
            _cacheService = cacheService;
        }

        public IEnumerable<MaritalStatus> GetAllStatuses()
        {
            //get data from cache
            var result = _cacheService.Get<IEnumerable<MaritalStatus>>("AllStatusesList");

            if (result == null)
            {
                //get data from dataBase if cache hasn't this data
                result = _rep.GetAllStatuses();

                if (result == null)
                {
                    throw new NullReferenceException("Не удалось получить список статусов!");
                }

                //put data into cache
                else _cacheService.Add("AllStatusesList", result, 7);
            }

            return result;

        }

        public MaritalStatus GetStatusByID(int id)
        {

            if (ArgumentHelper.IsValidID(id))
            {
                //get data from cache
                var result = _cacheService.Get<MaritalStatus>($"MaritalStatus{id}");

                if (result == null)
                {
                    //get data from dataBase if cache hasn't this data
                    result = _rep.GetStatusByID(id);

                    if (result == null)
                    {
                        throw new NullReferenceException($"Статус с идентификатором: {id} не найдено!");
                    }

                    //put data into cache
                    else _cacheService.Add($"MaritalStatus{id}", result, 300);
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
