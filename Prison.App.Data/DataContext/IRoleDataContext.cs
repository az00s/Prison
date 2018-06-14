﻿using Prison.App.Common.Entities;
using System.Collections.Generic;

namespace Prison.App.Data.DataContext
{
    public interface IRoleDataContext
    {
        IEnumerable<Role> GetAllRoles();
        Role GetRoleByID(int id);
        void Create(Role dtn);
        void Update(Role dtn);
        void Delete(int id);
    }
}