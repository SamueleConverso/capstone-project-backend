using Microsoft.AspNetCore.Identity;

namespace Capstone_Project.Models.Account
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> ApplicationUserRole
        {
            get; set;
        }
    }
}
