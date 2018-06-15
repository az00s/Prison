using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Repositories;
using System;


namespace Prison.App.Business.Services.Impl
{
    public class PlaceService: IPlaceService
    {
        private IPlaceOfStayRepository _rep;

        private ICachingService _cacheService;

        public PlaceService(IPlaceOfStayRepository rep, ICachingService cacheService)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IPlaceOfStayRepository");
            ArgumentHelper.ThrowExceptionIfNull(cacheService, "ICachingService");

            _rep = rep;
            _cacheService = cacheService;
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
