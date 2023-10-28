using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TodoLiveAi.Core;
using TodoLiveAi.Core.DbModels;

namespace TodoLiveAi.Web.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ContentExtra { get; set; }
        public string? Priority { get; set; }
        public string? FromRequested { get; set; }
        public string? State { get; set; }
        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateEdited { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime DateDue { get; set; }

        [ForeignKey("Owner")]
        public string? OwnerId { get; set; }
        public virtual AppUser? Owner { get; set; }
    }
}
