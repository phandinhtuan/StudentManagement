using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentManagement.DAL;
using StudentManagement.DAL.Service;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagement.Controllers
{
    public class UserRoleController : Controller
    {
        private StudentManagementDbContext db = new StudentManagementDbContext();
        // GET: UserRole
        public ActionResult Index()
        {
            // cho nay lay doi DB
            List<usp_AspNetUserRoles_GetList> listDB = new List<usp_AspNetUserRoles_GetList>();
            //ListUserRole item = new ListUserRole();
            //item.Name = "Test";
            //item.UserId = "Tuanpd";
            //listDB.Add(item);
            //return View(listDB);
            using(var service=new AspNetUserRolesService())
            {
                listDB = service.GetList();
            }

            return View(listDB);
        }
        public ActionResult Add()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Add(UserRole userRole)
        {
            StudentManagementDbContext context = new StudentManagementDbContext();
           
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            string id = "46ec55a7-af24-43fc-942b-40e477e57132";
            
            try
            {
                
                UserManager.AddToRole(id, userRole.RoleId);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            // Lam sao lu DB
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string userId,string roleId)
        {
            // chua viet gi
            //service Call DB
            int? result = null;
            using (var service = new AspNetUserRolesService())
            {
                result = service.DeleteUserRole(userId, roleId);
            }
            return RedirectToAction("Index");
        }
    }
}