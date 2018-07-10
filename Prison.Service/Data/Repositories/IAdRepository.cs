using System.Collections.Generic;

namespace Prison.AdvertismentService.Data.Repositories
{
    public interface IAdRepository
    {
        IReadOnlyCollection<Blurb> GetAll();
    }
}
