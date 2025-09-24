using ResolveIQ.Web.Data.Tasks;

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

        public UserTaskStatus Status { get; set; }
    }
}
