using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ResolveIQ.Data.Interfaces;
using ResolveIQ.Web.Data.Auth;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResolveIQ.Web.Data.Tasks
{
    public class UserTask : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DateCreated { get; set; }
        public int EffortPoints { get; set; }
        public UserTaskStatus Status { get; set; }
        
        [ForeignKey("Assignee")]
        public string AssigneeId { get; set; }

        [ForeignKey("Reporter")]
        public string ReporterId { get; set; }
               
        public AppUser Assignee { get; set; }
        public AppUser Reporter { get; set; }
    }

    public enum UserTaskStatus
    {
        Pending,
        InProgress,
        Completed
    }
}
