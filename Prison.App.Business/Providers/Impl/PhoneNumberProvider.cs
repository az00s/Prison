using Prison.App.Business.Services;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Prison.App.Business.Providers.Impl
{
    public class PhoneNumberProvider:IPhoneNumberProvider
    {
        private IPhoneNumberRepository _rep;

        private ICachingService _cacheService;

        public PhoneNumberProvider(IPhoneNumberRepository rep, ICachingService cacheService)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IPhoneNumberRepository");
            ArgumentHelper.ThrowExceptionIfNull(cacheService, "ICachingService");

            _rep = rep;
            _cacheService = cacheService;
        }

        public IEnumerable<PhoneNumber> GetAllNumbers()
        {
            //get data from cache
            var result = _cacheService.Get<IEnumerable<PhoneNumber>>("AllNumbersList");

            if (result == null)
            {
                //get data from dataBase if cache hasn't this data
                result = _rep.GetAllNumbers();

                if (result == null)
                {
                    throw new NullReferenceException("Не удалось получить список телефонов!");
                }

                //put data into cache
                else _cacheService.Add("AllNumbersList", result, 7);
            }

            return result;

        }

        public PhoneNumber GetNumberByID(int id)
        {

            if (ArgumentHelper.IsValidID(id))
            {
                //get data from cache
                var result = _cacheService.Get<PhoneNumber>($"PhoneNumber{id}");

                if (result == null)
                {
                    //get data from dataBase if cache hasn't this data
                    result = _rep.GetNumberByID(id);

                    if (result == null)
                    {
                        throw new NullReferenceException($"Телефон с идентификатором: {id} не найдено!");
                    }

                    //put data into cache
                    else _cacheService.Add($"PhoneNumber{id}", result, 15);
                }

                return result;
            }
            else
            {
                throw new ArgumentException($"Идентификатор номера телефона указан неверно.Пожалуйста укажите значение от 0 до {int.MaxValue}");
            }

        }

        public IEnumerable<Detainee> GetAllDetaineeLastNames()
        {
            return _rep.GetAllDetaineeLastNames();
        }
    }
}
