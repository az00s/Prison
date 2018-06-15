﻿using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IPlaceProvider
    {
        IEnumerable<PlaceOfStay> GetAllPlaces();
        PlaceOfStay GetPlaceByID(int id);
    }
}
