using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.DataContext
{
    public interface IDetentionDataContext
    {
        IReadOnlyCollection<Detention> GetAll();
        IReadOnlyCollection<Detention> GetDetentionsForLast3Days();
        Detention GetLast(int id);
        Detention GetByID(int id);
        void Create(Detention dtn);
    }
}