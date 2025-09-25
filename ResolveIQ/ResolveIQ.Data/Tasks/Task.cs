using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ResolveIQ.Data.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResolveIQ.Data.Tasks
{
    public class Task : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int EffortPoints { get; set; }
        
        [ForeignKey("Assignee")]
        public string AssigneeId { get; set; }

        [ForeignKey("Reporter")]
        public string ReporterId { get; set; }
               
        public IdentityUser Assignee { get; set; }
        public IdentityUser Reporter { get; set; }
    }
}
