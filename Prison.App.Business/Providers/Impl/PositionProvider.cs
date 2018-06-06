using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prison.App.Business.Services;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Repositories;

namespace Prison.App.Business.Providers.Impl
{
    public class PositionProvider : IPositionProvider
    {
        private IPositionRepository _rep;

        private ICachingService _cacheService;

        public PositionProvider(IPositionRepository rep, ICachingService cacheService)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IPositionRepository");
            ArgumentHelper.ThrowExceptionIfNull(cacheService, "ICachingService");

            _cacheService = cacheService;
            _rep = rep;
        }

        public IEnumerable<Position> GetAllPositions()
        {
            //get data from cache
            var result = _cacheService.Get<IEnumerable<Position>>("AllPositionList");

            if (result == null)
            {
                //get data from dataBase if cache hasn't this data
                result = _rep.GetAllPositions();

                if (result == null)
                {
                    throw new NullReferenceException("Не удалось получить список должностей!");
                }
                //because of positionss are not changing all time or could be changed rarely, cache for it was setted to 15 min
                else _cacheService.Add("AllPositionList", result, 900);
            }

            return result;
        }
    }
}
