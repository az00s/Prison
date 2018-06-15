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
        }

        public void Delete(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                _rep.Delete(id);
            }
            else
            {
                throw new ArgumentException($"Идентификатор Места содержания указан неверно.Пожалуйста укажите значение от 0 до {int.MaxValue}");
            }
        }

    }
}
