using System.ComponentModel.Design;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Dynamic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StudentManagement.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class StudentManagementDbContext : IdentityDbContext<ApplicationUser>
    {
        public StudentManagementDbContext()
            : base("StudentManagementConnection", throwIfV1Schema: false)
        {
        }

        public static StudentManagementDbContext Create()
        {
            return new StudentManagementDbContext();
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<RegisterCourse> RegisterCourses { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<RegisterClassRoom> RegisterClassRooms { get; set; }

        public System.Data.Entity.DbSet<StudentManagement.Models.usp_AspNetUser_GetList> usp_AspNetUser_GetList { get; set; }

        public System.Data.Entity.DbSet<StudentManagement.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public DbSet<AspNetUser> AspNetUsers { get; set; }
    }
}