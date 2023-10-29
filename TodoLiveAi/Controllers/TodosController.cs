using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoLiveAi.Core;
using TodoLiveAi.Infrastructure.Data;
using TodoLiveAi.Web.Models;

namespace TodoLiveAi.Web.Controllers
{
    public class TodosController : Controller
    {
        private readonly ILogger<TodosController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;

        public TodosController(ILogger<TodosController> logger, ApplicationDbContext dbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        public ApplicationDbContext Get_dbContext()
        {
            return _dbContext;
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
            // Get logged user details
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var vm = new MainVM();



            return View(vm);
        }


    }
}
