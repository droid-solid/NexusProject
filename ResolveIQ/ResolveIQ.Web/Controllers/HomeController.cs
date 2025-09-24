using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResolveIQ.Web.Data;
using ResolveIQ.Web.Data.Tasks;
using ResolveIQ.Web.Models;

namespace ResolveIQ.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; 
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAManager = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value == AppConstants.AppConstants.MANAGER_ROLE;

            var tasks = new List<TaskModel>();

            if (isAManager)
            {
                tasks = await _context.Tasks.Include(x => x.Assignee)
                                            .Include(x => x.Reporter)
                                            .Where(x => x.ReporterId == userId).Select(x => new TaskModel
                                            {
                                                Id = x.Id,
                                                Title = x.Title,
                                                Status = x.Status,
                                                Assignee = x.Assignee.FullName,
                                                Reporter = x.Reporter.FullName,
                                                DueDate = x.DueDate,
                                                DateCreated = x.DateCreated
                                            }).ToListAsync();
            }
            else
            {
                tasks = await _context.Tasks.Include(x => x.Assignee)
                                            .Include(x => x.Reporter)
                                            .Where(x => x.ReporterId == userId).Select(x => new TaskModel
                                            {
                                                Id = x.Id,
                                                Title = x.Title,
                                                Status = x.Status,
                                                Assignee = x.Assignee.FullName,
                                                Reporter = x.Reporter.FullName,
                                                DueDate = x.DueDate,
                                                DateCreated = x.DateCreated
                                            }).ToListAsync();
            }
            return View(tasks);
        }

        public async Task<IActionResult> GetTaskList()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAManager = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value == AppConstants.AppConstants.MANAGER_ROLE;
            var tasks = new List<TaskModel>();
            if (isAManager)
            {
                tasks = await _context.Tasks.Include(x => x.Assignee)
                                            .Include(x => x.Reporter)
                                            .Where(x => x.ReporterId == userId).Select(x => new TaskModel
                                            {
                                                Id = x.Id,
                                                Title = x.Title,
                                                Status = x.Status,
                                                Assignee = x.Assignee.FullName,
                                                Reporter = x.Reporter.FullName,
                                                DueDate = x.DueDate,
                                                DateCreated = x.DateCreated
                                            }).ToListAsync();
            }
            else
            {
                tasks = await _context.Tasks.Include(x => x.Assignee)
                                            .Include(x => x.Reporter)
                                            .Where(x => x.ReporterId == userId).Select(x => new TaskModel
                                            {
                                                Id = x.Id,
                                                Title = x.Title,
                                                Status = x.Status,
                                                Assignee = x.Assignee.FullName,
                                                Reporter = x.Reporter.FullName,
                                                DueDate = x.DueDate,
                                                DateCreated = x.DateCreated
                                            }).ToListAsync();
            }
            return PartialView(tasks);
        }

        public async Task<IActionResult> CreateTask()
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
