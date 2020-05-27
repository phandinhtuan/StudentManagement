using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    [Authorize]
    public class TeacherController : Controller
    {
        private StudentManagementDbContext db = new StudentManagementDbContext();

        // GET: Teacher/TeacherList
        public ActionResult TeacherList()
        {
            return View(db.Teachers.ToList());
        }

        // GET: Teacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }
        [Authorize(Roles = "Admin")]
        // GET: Teacher/Create
        public ActionResult Create()
        {
            // quyen tao la role= 1
            bool isCreatedSv = isRoleUser("Them Giao Vien");
            if (!isCreatedSv) return View("NoPermission");
            
            return View();
        }

       
        // POST: Teacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherID,FirstName,LastName,Qualification,Email,PhoneNumber,Gender")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("TeacherList");
            }

            return View(teacher);
        }
        [Authorize(Roles = "Admin")]
        // GET: Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // quyen tao la role= 1
            bool isEditSv = isRoleUser("Sua giao vien");
            if (!isEditSv) return View("NoPermission");
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherID,FirstName,LastName,Qualification,Email,PhoneNumber,Gender")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TeacherList");
            }
            return View(teacher);
        }
        [Authorize(Roles = "Admin")]
        // GET: Teacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // quyen tao la role= 1
            bool isDeleteSv = isRoleUser("Xoa Giao vien");
            if (!isDeleteSv) return View("NoPermission");
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }
        [Authorize(Roles = "Admin")]
        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("TeacherList");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
        public ActionResult Search(string Name = "")
        {
            var model = db.Teachers.Where(f => f.FirstName.Contains(Name) || f.LastName.Contains(Name)
            || f.PhoneNumber.Contains(Name) || f.Qualification.Contains(Name)).ToList();
            return PartialView(model);
        }
        //public JsonResult SearchName(string Name = "")
        //{
        //    var model = db.Teachers.Where(f => f.FirstName.Contains(Name) || f.LastName.Contains(Name)
        //    || f.PhoneNumber.Contains(Name) || f.Qualification.Contains(Name)).ToList();
        //    //var model = db.Teachers.Where(f => f.FirstName.Contains(Name) || f.LastName.Contains(Name)).FirstOrDefault();
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}
    }
}
