namespace TodoLiveAi.Web.Models
{
    public class MainVM
    {
        public TaskModel? TaskModel { get; set; }
        public List<TaskModel>? TaskList { get;}
        public TaskPriority? TaskPriority { get; set; }
        public List<TaskPriority> TaskPriorityList { get; set; }

        public MainVM()
        { 
            TaskModel = new TaskModel();

            TaskList = new List<TaskModel>();

            TaskPriority = new TaskPriority();

            TaskPriorityList = new List<TaskPriority>();
        }

    }
}
