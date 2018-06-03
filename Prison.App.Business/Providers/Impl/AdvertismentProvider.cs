using Prison.App.Business.Interfaces;
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

        public AdvertismentProvider(IAdService service)
        {
            ArgumentHelper.ThrowExceptionIfNull(service, "IAdService");

            _adService = service;
        }

        public IEnumerable<IBlurb> GetElementsFromRep(int numOfElements)
        {
            if (ArgumentHelper.IsValidNumber(numOfElements))
            {
                return _adService.GetElementsFromRep(numOfElements);
            }
            else
            {
                throw new ArgumentException($"Неверное число требуемых объявлений объявлений!");
            }
        }

    }
}
