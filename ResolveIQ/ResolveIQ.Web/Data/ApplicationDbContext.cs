using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResolveIQ.Web.Data.Auth;
using ResolveIQ.Web.Data.Tasks;

namespace ResolveIQ.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {            
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<UserTask> Tasks { get; set; }
    }
}
