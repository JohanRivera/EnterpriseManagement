using Microsoft.AspNetCore.Identity;

namespace EnterpriseManagement.Core.Entities.Auth
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
