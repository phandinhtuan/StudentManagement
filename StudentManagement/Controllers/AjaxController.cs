using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagement.Controllers
{
    public class AjaxController : Controller
    {
        StudentManagementDbContext context = new StudentManagementDbContext();
        // GET: Ajax
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string Name = "") 
        {
           var model= context.Teachers.Where(f => f.FirstName.Contains(Name) || f.LastName.Contains(Name)).ToList();
            return PartialView(model);
        }
        public JsonResult SearchName(string Name = "")
        {
            var model = context.Teachers.Where(f => f.FirstName.Contains(Name) || f.LastName.Contains(Name)).FirstOrDefault();
            return Json(model,JsonRequestBehavior.AllowGet);
        }
    }
}