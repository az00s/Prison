using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Repositories;
using System;

namespace Prison.App.Business.Services.Impl
{
    public class DetaineeService: IDetaineeService
    {
        private IDetaineeRepository _rep;

        private ICachingService _cacheService;

        public DetaineeService(IDetaineeRepository rep, ICachingService cacheService)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IDetaineeRepository");
            ArgumentHelper.ThrowExceptionIfNull(cacheService, "ICachingService");
            _rep = rep;
            _cacheService = cacheService;
        }

        public void ReleaseDetainee(Detention detention)
        {
            _rep.ReleaseDetainee(detention);
        }

        public void Create(Detainee dtn)
        {
            _rep.Create(dtn);

            //when new detainee added - detaineeList removed from cache because it's not valid
           _cacheService.Delete("AllDetaineeList");

        }

        public void Update(Detainee dtn)
        {
            _rep.Update(dtn);

            //when new data changed - detaineeList removed from cache because it's not valid
            _cacheService.Delete("AllDetaineeList");

        }

        public void Delete(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                _rep.Delete(id);

                //when new data changed - detaineeList removed from cache because it's not valid
                _cacheService.Delete("AllDetaineeList");
            }
            else
            {
                throw new ArgumentException($"Идентификатор задержанного указан неверно.Пожалуйста укажите значение от 0 до {int.MaxValue}");
            }
        }
    }
}
