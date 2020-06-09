using DocumentFormat.OpenXml.Wordprocessing;
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
using System.Web.Security;

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
            using (var service = new AspNetUserRolesService())
            {
                listDB = service.GetList();
            }
            // add dropdownlist userrole
            var viewModels = new List<usp_AspNetUser_GetList>();
            
            foreach (var user in listDB)
            {
                viewModels.Add(new usp_AspNetUser_GetList { Id=user.UserId, UserName = user.UserName });
            }
            SelectList selectUsers = new SelectList(viewModels,"Id", "UserName");
            ViewBag.userListDb = selectUsers;
            // chua toan nhan vien
            //ViewBag.ListAllUser = listDB;
            //Chua toan bo Role
            //ViewBag.ListAllRole = "List All Role";         
            foreach (var user in listDB)
            {
                viewModels.Add(new usp_AspNetUser_GetList { Id=user.RoleId,  UserName = user.Name});
            }
            SelectList selectRoles = new SelectList(viewModels, "Id", "UserName");
            ViewBag.listRoles = selectRoles;
            return View(listDB);
        }
        public ActionResult Add()
        {
            // viet hai service call DB 
            //1: lay het tat ca user trong he thong

            List<usp_AspNetUser_GetList> userList = new List<usp_AspNetUser_GetList>();
            //using (var service = new AspNetUserService())
            //{
            //    userList = service.GetListUser();
            //}
            //SelectList selectLists = new SelectList(userList, "Id", "UserName");

            var context = new StudentManagementDbContext();

            var allUsers = context.Users.ToList();

            var viewModels = new List<usp_AspNetUser_GetList>();

            foreach (var user in allUsers)
            {
                viewModels.Add(new usp_AspNetUser_GetList { Id = user.Id, UserName = user.UserName });
            }
            SelectList selectLists = new SelectList(viewModels, "Id", "UserName");
            ViewBag.userListDb = selectLists;

            //2: lay het tat ca role trong he thong 
            var allRoles = context.Roles.ToList();
            var viewRoles = new List<usp_AspNetUser_GetList>();
            foreach (var role in allRoles)
            {
                viewRoles.Add(new usp_AspNetUser_GetList { Id = role.Id, UserName = role.Name });
            }
            SelectList selectRoles = new SelectList(viewRoles, "Id", "UserName");
            ViewBag.listRoles = selectRoles;
            //cuoi cung dung ViewBag.ListUser,ViewBag.ListRole de truyen len view
            return RedirectToAction("Indexs") ;
        }
        [HttpPost]
        public ActionResult Add(UserRole userRole)
        {
            StudentManagementDbContext context = new StudentManagementDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            try
            {
                var allRoles = context.Roles.ToList();
                string rolesName = null;
                if (allRoles != null && allRoles.Count > 0)
                {
                    var item = allRoles.Where(s => s.Id == userRole.RoleId).FirstOrDefault();
                    rolesName = item.Name;
                }

                var result = UserManager.AddToRole(userRole.UserId, rolesName);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // Lam sao lu DB
            return RedirectToAction("Index");
        }
        public JsonResult Delete(string userId, string roleId)
        {
            // chua viet gi
            //service Call DB
            int? result = null;
            using (var service = new AspNetUserRolesService())
            {
                result = service.DeleteUserRole(userId, roleId);
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public JsonResult Search(string Name = "")
        {
           List<usp_AspNetUserRoles_GetList> listDB = new List<usp_AspNetUserRoles_GetList>();
            using (var service = new AspNetUserRolesService())
            {
                listDB = service.GetList();
            }

            var model = listDB.Where(u => u.UserName.Contains(Name) || u.Name.Contains(Name)).Select(u => new { UserName = u.UserName, Name = u.Name,UserId=u.UserId,RoleId=u.RoleId }).ToList() ;

            return Json(model,JsonRequestBehavior.AllowGet);
        }
        
    }
}