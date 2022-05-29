using System.ComponentModel.DataAnnotations;

namespace taskAPI.Models
{
    public class TaskDetail
    {        
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public TaskStatusEnum Status { get; set; }
        public float Progress { get; set; }
    }

    public enum TaskStatusEnum
    {
        ToDo,
        InProgress,
        Complete
    }
}
