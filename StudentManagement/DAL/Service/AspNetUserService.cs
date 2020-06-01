using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagement.DAL.Service
{
    public class AspNetUserService: GenericService<AspNetUser>
    {
        public List<usp_AspNetUser_GetList> GetListUser()
        {
            var lst = new List<usp_AspNetUser_GetList>();
            try
            {
                string strSql = "EXEC usp_AspNetUser_GetList";
                lst = _db.Database.SqlQuery<usp_AspNetUser_GetList>(strSql).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
    }
}