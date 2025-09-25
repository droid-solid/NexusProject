using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ResolveIQ.Web.Data;
using ResolveIQ.Web.Data.Tasks;
using ResolveIQ.Web.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ResolveIQ.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; 
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        
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
                                                DateCreated = x.DateCreated,
                                                TaskNumber = x.TaskNumber
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
                                                DateCreated = x.DateCreated,
                                                TaskNumber = x.TaskNumber
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
            ViewBag.Assignees = await GetAssigneesDropDownList();
            return View(new CreateTaskViewModel());
        }


        public async Task<IActionResult> EditTask(int id)
        {
            var task = await _context.Tasks.Select(x => new CreateTaskViewModel {
                Assignee = x.AssigneeId,
                Id = x.Id,
                Description = x.Description,
                DueDate = x.DueDate,
                EffortPoints = x.EffortPoints,
                Priority = x.Priority,
                Status = x.Status,
                Title = x.Title,
                TaskNumber = x.TaskNumber
            }).FirstOrDefaultAsync(x => x.Id == id);
            if(task == null)
            {
                return new NotFoundResult();
            }
            ViewBag.Assignees = await GetAssigneesDropDownList();
            return View("CreateTask",task);
        }

        public async Task<IActionResult> Task(string taskNumber)
        {
            var task = await _context.Tasks.Include(x => x.Assignee)
                                            .Include(x => x.Reporter)
                                            .Where(x => x.TaskNumber == taskNumber).Select(x => new TaskModel
                                            {
                                                Id = x.Id,
                                                Title = x.Title,
                                                Status = x.Status,
                                                Assignee = x.Assignee.FullName,
                                                Reporter = x.Reporter.FullName,
                                                DueDate = x.DueDate,
                                                DateCreated = x.DateCreated,
                                                Description = x.Description,
                                                EffortPoints = x.EffortPoints,
                                                Priority = x.Priority,
                                                TaskNumber = x.TaskNumber                                                
                                            }).FirstOrDefaultAsync();
            if (task == null)
            {
                return new NotFoundResult();
            }

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> EditTask(CreateTaskViewModel taskmodel)
        {
            ViewBag.Assignees = await GetAssigneesDropDownList();
            var task = _context.Tasks.FirstOrDefault(x => x.Id == taskmodel.Id);
            if(task == null)
            {
                return new NotFoundResult();
            }
            else
            {
                task.AssigneeId = taskmodel.Assignee;
                task.Description = taskmodel.Description;
                task.DueDate = taskmodel.DueDate;
                task.EffortPoints = taskmodel.EffortPoints;
                task.Priority = taskmodel.Priority;
                task.Status = taskmodel.Status;
                task.Title = taskmodel.Title;

                _context.Update(task);
                await _context.SaveChangesAsync();
                ViewBag.Notification = new { success = true, message = "New task has been created successfully" };
                return RedirectToAction("Index");
            }           
        }


        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskViewModel task)
        {
            ViewBag.Assignees = await GetAssigneesDropDownList();
            if (ModelState.IsValid)
            {
                using(var transaction = _context.Database.BeginTransaction())
                {
                    string userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null;
                    var newTask = new UserTask
                    {
                        AssigneeId = task.Assignee,
                        DueDate = task.DueDate,
                        DateCreated = DateTime.Now,
                        Description = task.Description,
                        EffortPoints = task.EffortPoints,
                        Priority = task.Priority,
                        ReporterId = userId,
                        Status = task.Status,
                        Title = task.Title,
                        TaskNumber = task.TaskNumber
                    };
                    _context.Tasks.Add(newTask);
                    await _context.SaveChangesAsync();
                    newTask.TaskNumber = $"RES-{newTask.Id}";
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    ViewBag.Notification = new { success = true, message = "New task has been created successfully" };
                    return View(new CreateTaskViewModel());
                }                                               
            }
            return View(task);
        }

        private async Task<IList<SelectListItem>> GetAssigneesDropDownList()
        {
            var managerRole = await _context.Roles.FirstOrDefaultAsync(x => x.Name == AppConstants.AppConstants.MANAGER_ROLE);
            var userSelect = await (from userRole in _context.UserRoles.AsNoTracking()
                                    join user in _context.AppUsers.AsNoTracking() on userRole.UserId equals user.Id
                                    where userRole.RoleId != managerRole.Id
                                    select new SelectListItem
                                    {
                                        Text = user.FullName,
                                        Value = user.Id,
                                    }).ToListAsync();

            userSelect.Insert(0, new SelectListItem { Value = "", Text = "-Choose Assignee-" });
            return userSelect;
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
