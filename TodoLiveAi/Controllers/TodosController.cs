using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoLiveAi.Core;
using TodoLiveAi.Core.DbModels;
using TodoLiveAi.Infrastructure.Data;
using TodoLiveAi.Service;
using TodoLiveAi.Web.Models;

namespace TodoLiveAi.Web.Controllers
{
    public class TodosController : Controller
    {
        private readonly ILogger<TodosController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        private ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TodosController(ILogger<TodosController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config, ITaskRepository taskRepository, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                string? userId = _userManager.GetUserId(User);

                if (userId == null)
                {
                    return Content("Error, user id does not exist!");
                }

                var allTasks = await _taskRepository.GetAllUserTasks(userId);

                var incompleteTasks = allTasks.Where(task => task.State != "Completed").ToList();

                var priorities = await _taskRepository.GetPriorities();

                var vm = new MainVM
                {
                    TaskList = incompleteTasks.Select(_mapper.Map<TaskModel>).ToList(),
                    TaskPriorityList = priorities.Select(_mapper.Map<TaskPriorityModel>).ToList(),
                };

                return View(vm);

            }
            catch (Exception)
            {
                var priorities = await _taskRepository.GetPriorities();
                var vm = new MainVM
                {
                    TaskPriorityList = priorities.Select(_mapper.Map<TaskPriorityModel>).ToList(),
                };

                return View(vm);
            }
        }







        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskModel formContent)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Invalid form data!";

                return RedirectToAction("Error");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var newTask = new TaskModel
            {
                Title = formContent.Title,
                Content = formContent.Content,
                FromRequested = formContent.FromRequested,
                Priority = formContent.Priority,
                DateRequested = DateTime.Now,
                DateDue = formContent.DateDue,
                State = "Not started",
                OwnerId = userId,
            };

            var createdTask = await _taskRepository.CreateTask(_mapper.Map<TaskDB>(newTask));
            newTask.Id = createdTask.Id;

            var priorities = await _taskRepository.GetPriorities();

            var vm = new MainVM
            {
                TaskList = new List<TaskModel> { newTask },
                TaskPriorityList = priorities.Select(_mapper.Map<TaskPriorityModel>).ToList(),
            };

            return PartialView("_Task", vm);
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteTask(string cardId)
        {
            string? userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return Content("Error, user id does not exist!");
            }

            await _taskRepository.DeleteTask(cardId, userId);

            return RedirectToAction("Index");
        }





        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MarkTaskAsComplete(string cardId)
        {
            string? userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return Content("Error, user id does not exist!");
            }

            var task = await _taskRepository.GetTaskById(Int32.Parse(cardId));

            if (task == null || task.OwnerId != userId)
            {
                return Content("Error - task does not exist or belongs to someone else.");
            }


            task.DateCompleted = DateTime.Now;
            task.DateEdited = DateTime.Now;
            task.State = "Completed";

            await _taskRepository.UpdateTask(task);

            return RedirectToAction("Index");

        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ShowSelectedTasks(string id)
        {
            string? userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return Content("Error, user not set!");
            }

            var allTasks = await _taskRepository.GetAllUserTasks(userId);
            var priorities = await _taskRepository.GetPriorities();

            var vm = new MainVM
            {
                TaskList = allTasks.Select(_mapper.Map<TaskModel>).ToList(),
                TaskPriorityList = priorities.Select(_mapper.Map<TaskPriorityModel>).ToList(),
            };

            switch (id)
            {
                case "Completed":
                    vm.TaskList = vm.TaskList.Where(x => x.State == "Completed" && x.DateDue >= DateTime.UtcNow).ToList();
                    break;
                case "Overdue":
                    vm.TaskList = vm.TaskList.Where(x => x.State != "Completed" && x.DateDue < DateTime.UtcNow).ToList();
                    break;
                case "Urgent":
                    vm.TaskList = vm.TaskList.Where(x => x.Priority == "Urgent" && x.State != "Completed").ToList();
                    break;
                case "Important":
                    vm.TaskList = vm.TaskList.Where(x => x.Priority == "Important" && x.State != "Completed").ToList();
                    break;
                case "Medium":
                    vm.TaskList = vm.TaskList.Where(x => x.Priority == "Medium" && x.State != "Completed").ToList();
                    break;
                case "Low":
                    vm.TaskList = vm.TaskList.Where(x => x.Priority == "Low" && x.State != "Completed").ToList();
                    break;
                default:
                    // Handle unknown id
                    break;
            }

            return PartialView("_Task", vm);
        }





    }
}
