using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using Prison.App.Data.Repositories;

namespace Prison.App.Business.Providers.Impl
{
    public class PositionProvider : IPositionProvider
    {
        private ILogger _log;

        private IPositionRepository _rep;

        public PositionProvider(ILogger log, IPositionRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");
            ArgumentHelper.ThrowExceptionIfNull(rep, "IPositionRepository");

            _log = log;
            _rep = rep;
        }

        public IEnumerable<Position> GetAllRecordsFromTable()
        {
            return _rep.GetAllRecordsFromTable();
        }
    }
}
