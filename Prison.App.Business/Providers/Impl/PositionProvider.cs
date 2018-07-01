using System;
using System.Collections.Generic;
using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using Prison.App.Data.Repositories;

namespace Prison.App.Business.Providers.Impl
{
    public class PositionProvider : IPositionProvider
    {
        private IPositionRepository _rep;

        public PositionProvider(IPositionRepository rep)
        {
            ArgumentHelper.ThrowExceptionIfNull(rep, "IPositionRepository");

            _rep = rep;
        }

        public IReadOnlyCollection<Position> GetAllPositions()
        {
            var result = _rep.GetAllPositions();

            if (result == null)
            {
                throw new NullReferenceException("Не удалось получить список должностей!");
            }

        return result;
        }

        public Position GetPositionByID(int id)
        {
            if (ArgumentHelper.IsValidID(id))
            {
                var result = _rep.GetPositionByID(id);

                if (result == null)
                {
                    throw new NullReferenceException($"Должность с идентификатором: {id} не найдено!");
                }

                return result;
            }
            else
            {
                throw new ArgumentException($"Идентификатор должности указан неверно.Пожалуйста укажите значение от 0 до {int.MaxValue}");
            }
        }
    }
}
