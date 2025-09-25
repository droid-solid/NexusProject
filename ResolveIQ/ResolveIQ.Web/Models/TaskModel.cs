using ResolveIQ.Web.Data.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResolveIQ.Web.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DueDate { get; set; }
        public string Assignee { get; set; }
        public string Reporter { get; set; }
        public string Description { get; set; }
        public int EffortPoints { get; set; }
        public string TaskNumber { get; set; }
        public UserTaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
    }

    public class CreateTaskViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime DueDate { get; set; } = DateTime.Now;
        [Required]
        public string Assignee { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int EffortPoints { get; set; }
        [Required]
        public UserTaskStatus Status { get; set; }
        [Required]
        public TaskPriority Priority { get; set; }
        public string TaskNumber { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
    }


    public enum ChartMode
    {
        TaksCount = 1,
        EffortPoints       
    }

    public class UserSelect
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
