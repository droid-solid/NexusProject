using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResolveIQ.Web.Data.Auth;
using ResolveIQ.Web.Data.Notification;
using ResolveIQ.Web.Data.Tasks;
using ResolveIQ.Web.Models;

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
        public DbSet<UserDevice> Devices { get; set; }
    }
}
