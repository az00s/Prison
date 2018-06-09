using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Repositories;
using System;

namespace Prison.App.Business.Services.Impl
{
    class PhoneNumberService:IPhoneNumberService
    {
        private IPhoneNumberRepository _rep;

        private ICachingService _cacheService;

        public PhoneNumberService(IPhoneNumberRepository rep, ICachingService cacheService)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IPhoneNumberRepository");
            ArgumentHelper.ThrowExceptionIfNull(cacheService, "ICachingService");

            _rep = rep;
            _cacheService = cacheService;
        }

        public void Create(PhoneNumber plc)
        {
            _rep.Create(plc);
        }

        public void Update(PhoneNumber plc)
        {
            _rep.Update(plc);

            if (_cacheService.Contains($"PhoneNumber{plc.NumberID}"))
            {
                _cacheService.Update($"PhoneNumber{plc.NumberID}", plc, 15);
            }

            else //put data into cache
                _cacheService.Add($"PhoneNumber{plc.NumberID}", plc, 15);
        }

        public void Delete(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                _rep.Delete(id);

                _cacheService.Delete($"PhoneNumber{id}");

            }
            else
            {
                throw new ArgumentException($"Идентификатор номера телефона указан неверно.Пожалуйста укажите значение от 0 до {int.MaxValue}");
            }
        }
    }
}
