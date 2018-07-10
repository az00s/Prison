using Ninject.Modules;
using Prison.AdvertismentService.Business;
using Prison.AdvertismentService.Data.Repositories;
using Prison.AdvertismentService.Repositories;

namespace Prison.AdvertismentService.Ioc
{
    internal class DIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAdRepository>().To<AdRepository>();
            Bind<IAdProvider>().To<AdProvider>(); 
        }
    }
}