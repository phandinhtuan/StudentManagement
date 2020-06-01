using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentManagement.DAL.Service
{
    public class AspNetUserRolesService: GenericService<AspNetUserRoles>
    {
        public List<usp_AspNetUserRoles_GetList> GetList()
        {
            var lst = new List<usp_AspNetUserRoles_GetList>();
            try
            {
                string strSql = "EXEC usp_AspNetUserRoles_GetList";
                lst = _db.Database.SqlQuery<usp_AspNetUserRoles_GetList>(strSql).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public int? DeleteUserRole(string userId,string roleId)
        {
            int? rs = null;
            try
            {
                
                var prUserId = new SqlParameter("@userId", System.Data.SqlDbType.NVarChar, 128)
                {
                    Value = string.IsNullOrWhiteSpace(userId) ? (object)DBNull.Value : userId
                };
                var prRoleId = new SqlParameter("@roleId", System.Data.SqlDbType.NVarChar, 128)
                {
                    Value = string.IsNullOrWhiteSpace(roleId) ? (object)DBNull.Value : roleId
                };
                var prResult = new SqlParameter()
                {
                    ParameterName = "@result",
                    SqlDbType = System.Data.SqlDbType.TinyInt,
                    Direction = System.Data.ParameterDirection.Output
                };
                string strSql = "EXEC usp_AspNetUserRoles_DeleteUserRole @userId, @roleId, @result OUTPUT";
                _db.Database.ExecuteSqlCommand(strSql, prUserId, prRoleId, prResult);
                rs =Convert.ToInt32(prResult.Value);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return rs;
        }
        

    }
}