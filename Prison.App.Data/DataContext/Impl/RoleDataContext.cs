using Prison.App.Common.Entities;
using Prison.App.Common.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Prison.App.Data.DataContext.Impl
{
    internal class RoleDataContext:IRoleDataContext
    {
        private IDataContext<Role> _context;

        public RoleDataContext(IDataContext<Role> context)
        {
            ArgumentHelper.ThrowExceptionIfNull(context, "IDataContext<Role>");

            _context = context;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            IEnumerable<Role> roleList = new List<Role>();

            var dataSet = _context.ExecuteQuery("SelectAllRoles", null, CommandType.StoredProcedure);

            roleList = ToRoleList(dataSet);

            return roleList;
        }

        public Role GetRoleByID(int id)
        {
            Role role;

            IDictionary<string, object> parameters = new Dictionary<string, object> { { "@ID", id } };

            var dataSet = _context.ExecuteQuery("SelectRoleByID", parameters, CommandType.StoredProcedure);

            role = ToRole(dataSet);

            return role;

        }

        public void Create(Role dtn)
        {
            IDictionary<string, object> parameters =
                new Dictionary<string, object>
                {
                    { "@RoleName", dtn.RoleName },
                };

            _context.ExecuteNonQuery("CreateRole", parameters, CommandType.StoredProcedure);
        }

        public void Update(Role dtn)
        {
            IDictionary<string, object> parameters =
                 new Dictionary<string, object>
                 {
                    { "@ID", dtn.RoleID },
                    { "@RoleName", dtn.RoleName },
                 };

            _context.ExecuteNonQuery("UpdateRole", parameters, CommandType.StoredProcedure);
        }

        public void Delete(int id)
        {
            IDictionary<string, object> parameters =
                new Dictionary<string, object>
                {
                    { "@ID", id },
                };

            _context.ExecuteNonQuery("DeleteRole", parameters, CommandType.StoredProcedure);
        }



        #region Converters
        private IEnumerable<Role> ToRoleList(DataSet dataset)
        {
            List<Role> list = new List<Role>();

            var roleTable = dataset.Tables[0];

            foreach (var row in roleTable.AsEnumerable())
            {
                list.Add(new Role
                {
                    RoleID = row.Field<int>("RoleID"),
                    RoleName = row.Field<string>("RoleName")
                });
            }
            return list;
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
