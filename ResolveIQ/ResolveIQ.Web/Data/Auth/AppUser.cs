using Microsoft.AspNetCore.Identity;

namespace ResolveIQ.Web.Data.Auth
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
