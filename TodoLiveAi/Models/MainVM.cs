namespace TodoLiveAi.Web.Models
{
    public class MainVM
    {
        public TaskModel? TaskModel { get; set; }
        public List<TaskModel>? TaskList { get;}

        public MainVM()
        { 
            TaskModel = new TaskModel();

            TaskList = new List<TaskModel>();
        }

    }
}
