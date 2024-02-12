namespace TodoLiveAi.Web.Models
{
    public class MainVM
    {
        public TaskModel? TaskModel { get; set; }
        public List<TaskModel>? TaskList { get; set; }
        public TaskPriorityModel? TaskPriority { get; set; }
        public List<TaskPriorityModel> TaskPriorityList { get; set; }

        public MainVM()
        { 
            TaskModel = new TaskModel();

            TaskList = new List<TaskModel>();

            TaskPriority = new TaskPriorityModel();

            TaskPriorityList = new List<TaskPriorityModel>();
        }

    }
}
