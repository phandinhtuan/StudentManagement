using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentManagement.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        // GET: Role
        StudentManagementDbContext context = new StudentManagementDbContext();
        
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!isAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            Boolean result = User.IsInRole("Admin");
            var Roles = context.Roles.ToList();
            return View(Roles);
            
        }

        public bool isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                StudentManagementDbContext context = new StudentManagementDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                bool result = false;
                foreach (var item in s)
                    if (item == "Admin")
                        result = true;
                return result;
            }
            return false;
        }
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {


                if (!isAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            var Role = new IdentityRole();
            return View(Role);
        }

        
        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!isAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public bool isRoleUser(string role)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                StudentManagementDbContext context = new StudentManagementDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                bool result = false;
                foreach (var item in s)
                    if (item == role)
                        result = true;
                return result;
            }
            return false;
        }
    }
}