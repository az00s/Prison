using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Business.Providers.Impl
{
    public class DetentionProvider: IDetentionProvider
    {
        private IDetentionRepository _rep;

        public DetentionProvider(IDetentionRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IDetentionRepository");
            _rep = rep;
        }

        public IReadOnlyCollection<Detention> GetAll()
        {
            return _rep.GetAll();
        }

        public IReadOnlyCollection<Detention> GetDetentionsForLast3Days()
        {
            return _rep.GetDetentionsForLast3Days();
        }

        public Detention GetByID(int id)
        {
            return _rep.GetByID(id);
        }

        public Detention GetLast(int id)
        {
            return _rep.GetLast(id);
        }


    }
}
