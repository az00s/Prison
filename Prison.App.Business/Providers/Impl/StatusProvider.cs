using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Prison.App.Business.Providers.Impl
{
    class StatusProvider:IStatusProvider
    {
        private IStatusRepository _rep;

        public StatusProvider(IStatusRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IStatusRepository");

            _rep = rep;
        }

        public IReadOnlyCollection<MaritalStatus> GetAllStatuses()
        {
            var result = _rep.GetAllStatuses();

            if (result == null)
            {
                throw new NullReferenceException("Не удалось получить список статусов!");
            }

            return result;
        }

        public MaritalStatus GetStatusByID(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var result = _rep.GetStatusByID(id);

                if (result == null)
                {
                    throw new NullReferenceException($"Статус с идентификатором: {id} не найдено!");
                }

                return result;
            }
            else
            {
                throw new ArgumentException($"Идентификатор Места содержания указан неверно.Пожалуйста укажите значение от 0 до {int.MaxValue}");
            }
        }
    }
}
