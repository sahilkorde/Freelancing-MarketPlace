using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Trial3.Areas.Identity.Data;
using Trial3.Models;
using System.Data.Entity;
using System.Security.Claims;

namespace Trial3.Controllers
{
    
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string Getuserid() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public ProjectController(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _Db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index(string? term, int? minbudget, int? maxbudget, string? tags)
        {
            if(term == null && minbudget == null && maxbudget == null && tags == null)
            {
                var project = _Db.Projects.AsNoTracking().ToList();
                return View(project);
            }
            var projects = _Db.Projects
                .Where(x => x.ProjectName.Contains(term) || x.tags.Contains(term) || x.tags.Contains(tags)).AsNoTracking().ToList();

            return View(projects);
        }
/*
        [HttpPost]
        public IActionResult Index(string Term)
        {
            var project = _Db.Projects.Where(x => x.ProjectName == Term || x.tags.Contains(Term));
            return View();
        }
        public IActionResult Index(int minbug, int maxbug, string tags)
        {
            var project = _Db.Projects.Where(x => x.CreateTime > DateTime.Now && x.tags.Contains(tags));
            return View();
        }*/

        [HttpGet]
        [Authorize(Roles = UserRoles.Frellancer + ", " + UserRoles.Employer)]
        public async Task<IActionResult> UserProject(string? id)
        {
            if (id == null)
            {
                return RedirectToAction("index");
            }
            var employer = await _userManager.GetUserAsync(User);
            /*var project1 = _Db.Entry(await _userManager.GetUserAsync(User)).Collection(c => c.UserProjects);
            var project2 = project1.LoadAsync();*/
            var userProject = _Db.Entry(employer)
                .Collection(e => e.UserProjects)
                .Query()
                .ToList();
            //var project = _Db.Projects.Where(r => r.EmployerId == id);
            return View(userProject);
        }
        [HttpGet]
        public IActionResult ProjectBidDetail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var project = _Db.Projects.Find(id);
            var projectbids = _Db.Entry(project)
                .Collection(e => e.ProjectBids)
                .Query()
                .ToList();
            //var bid = _Db.Bids.Where(r=>r.ProjectId==id);
            var projectbid = new ProjectBidView();
            projectbid.Projects = project;
            projectbid.Bids = projectbids;
            return View(projectbid);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Frellancer + ", " + UserRoles.Employer)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoles.Frellancer + ", " + UserRoles.Employer)]
        public IActionResult Create(Project project)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CStatus = true;
                return View(project);
            }
            Project proj = new Project
            {
                ProjectName = project.ProjectName,
                Description = project.Description,
                Status = "Active",
                tags = project.tags,
                EmployerId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                CreateTime = DateTime.Now,
            };
            _Db.Projects.Add(proj);
            _Db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Detail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var project = _Db.Projects.FindAsync(id);
            return View(project);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Frellancer + ", " + UserRoles.Employer)]
        public IActionResult Edit(int? projId)
        {            
            if(projId == null || projId == 0)
            {
                return NotFound();
            }
            var project = _Db.Projects.Find(projId);
            var id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != project.EmployerId)
            {
                return NotFound();
            }
            return View(project);
                
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoles.Frellancer + ", " + UserRoles.Employer)]
        public IActionResult Edit(Project project)
        {
            if (!ModelState.IsValid)
            {
                return View(project);
            }
            _Db.Projects.Update(project);
            _Db.SaveChanges();
            return RedirectToAction("Index","Project");
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Frellancer)]
        public IActionResult MakeBid(string? projectid)
        {
            if (projectid == null)
            {
                return NotFound();
            }
            ViewBag.project_id = projectid;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoles.Frellancer)]
        public async Task<IActionResult> MakeBid(Bid bid)
        {
            if (!ModelState.IsValid)
            {
                return View(bid);
            }
            var employer = await _userManager.GetUserAsync(User);
            var projectid = bid.ProjectId;
            var project = _Db.Projects.Find(projectid);
            var projectbids = _Db.Entry(project)
                .Collection(p => p.ProjectBids)
                .Query()
                .ToList();
            //projectbids.FirstOrDefault(x => x.FreelancerId == bid.FreelancerId);
            foreach (Bid bids in projectbids)
            {
                if (bids.FreelancerId == employer.Id)
                {
                    ViewBag.mbstatus = false;
                    return RedirectToAction("index", "home");
                }
            }
            _Db.Bids.Add(bid);
            _Db.SaveChanges();
            ViewBag.mbstatus = true;
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Employer + ", " + UserRoles.Frellancer)]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }
            Project project = await _Db.Projects.FindAsync(id);
            if (project.EmployerId != Getuserid())
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoles.Employer + ", " + UserRoles.Frellancer)]
        public IActionResult Delete(Project project)
        {
            if (project.EmployerId != Getuserid())
            {
                return NotFound();
            }
            if (project == null)
            {
                ViewBag.status = "Failed to Delete Project";
                return RedirectToAction("Index");
            }
            _Db.Projects.Remove(project);
            ViewBag.status = "Project Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
