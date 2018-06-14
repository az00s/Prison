using Prison.App.Common.Entities;

namespace Prison.App.Business.Services
{
    public interface IDetaineeService
    {
        void ReleaseDetainee(Detention detention);
        void Create(Detainee dtn);
        void Update(Detainee dtn);
        void Delete(int id);
    }
}
