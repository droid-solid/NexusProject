using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResolveIQ.Data.Context
{
    public class ApplicationContext : IdentityDbContext
    {
        public DbSet<Task> Tasks { get; set; }
    }
}
