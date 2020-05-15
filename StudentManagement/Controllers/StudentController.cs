using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private StudentManagementDbContext db = new StudentManagementDbContext();

        // GET: Student/StudentList
        public ActionResult StudentList()
        {
            var list = db.Students.ToList();
            if(list!=null && list.Count>0)
            {
                foreach (var item in list)
                    item.FullName =item.FirstName +" "+ item.LastName;
            }
            return View(list);
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        
        // GET: Student/Create
        public ActionResult Create()
        {
            // quyen tao la role:Them Sinh vien
            bool isCreatedSv = isRoleUser("Them Sinh vien");
            if (!isCreatedSv) return View("NoPermission");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,Gender,Birthday,Email,PhoneNumber")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("StudentList");
            }

            return View(student);
        }
       
        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // quyen tao la role= 1
            bool isEditSv = isRoleUser("Sua sinh vien");
            if (!isEditSv) return View("NoPermission");

            Student student = db.Students.Find(id);
           
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {


            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("StudentList");
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // quyen tao la role= 1
            bool isDeleteSv = isRoleUser("Xoa Sinh Vien");
            if (!isDeleteSv) return View("NoPermission");

            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("StudentList");
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
    }
}
