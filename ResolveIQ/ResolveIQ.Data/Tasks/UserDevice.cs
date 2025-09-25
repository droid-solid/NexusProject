using Microsoft.AspNetCore.Identity;
using ResolveIQ.Data.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResolveIQ.Data.Tasks
{
    public class UserDevice : IEntity
    {
        public int Id { get; set; }
        public string DeviceToken { get; set; }
        public string DeviceType { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public IdentityUser User { get; set; }
    }
}
