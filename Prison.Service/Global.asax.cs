using Ninject;
using Ninject.Extensions.Wcf;
using Prison.AdvertismentService.Ioc;

namespace Prison.AdvertismentService
{
    public class Global : NinjectWcfApplication
    {
        protected override IKernel CreateKernel()
        {
            return new StandardKernel(new DIModule());
        }
    }
}