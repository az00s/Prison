using Prison.App.Business.Interfaces;
using Prison.App.Business.Services;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public class AdvertismentProvider:IAdvertismentProvider
    {
        private IAdService _adService;

        private ICachingService _cacheService;

        public AdvertismentProvider(IAdService service, ICachingService cacheService)
        {
            ArgumentHelper.ThrowExceptionIfNull(service, "IAdService");
            ArgumentHelper.ThrowExceptionIfNull(cacheService, "ICachingService");

            _adService = service;
            _cacheService = cacheService;
        }

        public IEnumerable<IBlurb> GetAds(int numOfElements)
        {
            if (ArgumentHelper.IsValidNumber(numOfElements))
            {
                var result = _cacheService.Get<IEnumerable<IBlurb>>("Ads");

                if (result == null)
                {
                    result=_adService.GetAds(numOfElements);

                    if (result != null)
                    {
                        _cacheService.Add("Ads", result, 20);
                    }
                }

                return result;
            }
            else
            {
                throw new ArgumentException($"Неверное число требуемых объявлений!");
            }
        }

    }
}
