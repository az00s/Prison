using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Business.Providers
{
    public interface IDetentionProvider
    {
        IReadOnlyCollection<Detention> GetAll();
        IReadOnlyCollection<Detention> GetDetentionsForLast3Days();
        Detention GetLast(int id);
        Detention GetByID(int id);
    }
}
