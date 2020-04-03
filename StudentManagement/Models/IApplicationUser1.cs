using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public interface IApplicationUser1
    {
        int AccessFailedCount { get; set; }
        ICollection<IdentityUserClaim> Claims { get; }
        string Email { get; set; }
        string Email { get; set; }
        bool EmailConfirmed { get; set; }
        string FirstName { get; set; }
        ICollection<ApplicationUserGroup> Groups { get; set; }
        string Id { get; set; }
        string LastName { get; set; }
        bool LockoutEnabled { get; set; }
        DateTime? LockoutEndDateUtc { get; set; }
        ICollection<IdentityUserLogin> Logins { get; }
        string PasswordHash { get; set; }
        string PhoneNumber { get; set; }
        bool PhoneNumberConfirmed { get; set; }
        ICollection<IdentityUserRole> Roles { get; }
        string SecurityStamp { get; set; }
        bool TwoFactorEnabled { get; set; }
        string UserName { get; set; }

        bool Equals(object obj);
        Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager);
        int GetHashCode();
        string ToString();
    }
}