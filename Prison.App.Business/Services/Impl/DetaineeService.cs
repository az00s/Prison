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
        }

        public void Update(Detainee dtn)
        {
            _rep.Update(dtn);

            if (_cacheService.Contains($"Detainee{dtn.DetaineeID}"))
            {
                _cacheService.Update($"Detainee{dtn.DetaineeID}", dtn, 60);
            }

            else //put data into cache
                _cacheService.Add($"Detainee{dtn.DetaineeID}", dtn, 60);
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


    }
}
