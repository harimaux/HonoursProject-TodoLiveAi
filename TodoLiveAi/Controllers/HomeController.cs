using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using OpenAI_API.Completions;
using OpenAI_API;
using System.Diagnostics;
using System.Security.Claims;
using TodoLiveAi.Core;
using TodoLiveAi.Core.DbModels;
using TodoLiveAi.Models;
using TodoLiveAi.Service;
using TodoLiveAi.Web.Models;

namespace TodoLiveAi.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, ITaskRepository taskRepository, IMapper mapper, IConfiguration config)
        {
            _logger = logger;
            _userManager = userManager;
            _taskRepository = taskRepository;
            _mapper = mapper;
            _config = config;
        }




        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}