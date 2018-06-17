using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Prison.App.Data.DataContext.Impl
{
    internal class RoleDataContext:IRoleDataContext
    {
        private readonly IDataContext<Role> _context;

        public RoleDataContext(IDataContext<Role> context)
        {
            ArgumentHelper.ThrowExceptionIfNull(context, "IDataContext<Role>");

            _context = context;
        }

        public IReadOnlyCollection<Role> GetAllRoles()
        {
            var dataSet = _context.ExecuteQuery("SelectAllRoles", null, CommandType.StoredProcedure);

            var roleList = ToRoleList(dataSet);

            return roleList;
        }

        public Role GetRoleByID(int id)
        {
            var parameters = new Dictionary<string, object> { { "@ID", id } };

            var dataSet = _context.ExecuteQuery("SelectRoleByID", parameters, CommandType.StoredProcedure);

            var role = ToRole(dataSet);

            return role;
        }

        public void Create(Role dtn)
        {
            var parameters =
                new Dictionary<string, object>
                {
                    { "@RoleName", dtn.RoleName },
                };

            _context.ExecuteNonQuery("CreateRole", parameters, CommandType.StoredProcedure);
        }

        public void Update(Role dtn)
        {
            var parameters =
                 new Dictionary<string, object>
                 {
                    { "@ID", dtn.RoleID },
                    { "@RoleName", dtn.RoleName },
                 };

            _context.ExecuteNonQuery("UpdateRole", parameters, CommandType.StoredProcedure);
        }

        public void Delete(int id)
        {
            var parameters =
                new Dictionary<string, object>
                {
                    { "@ID", id },
                };

            _context.ExecuteNonQuery("DeleteRole", parameters, CommandType.StoredProcedure);
        }

        #region Converters
        private IReadOnlyCollection<Role> ToRoleList(DataSet dataset)
        {
           return dataset.Tables[0].AsEnumerable().Select(row=>
                new Role
                {
                    RoleID = row.Field<int>("RoleID"),
                    RoleName = row.Field<string>("RoleName")
                }
            ).ToList();
        }

        private Role ToRole(DataSet dataset)
        {
            var row = dataset.Tables[0].Rows[0];

            return new Role
            {
                RoleID = row.Field<int>("RoleID"),
                RoleName = row.Field<string>("RoleName"),

            };
        }

        #endregion
    }
}
