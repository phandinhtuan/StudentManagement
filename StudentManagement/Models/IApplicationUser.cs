using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public interface IApplicationUser
    {
        Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager);
    }
}