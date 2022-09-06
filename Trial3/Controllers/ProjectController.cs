using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Trial3.Areas.Identity.Data;
using Trial3.Models;

namespace Trial3.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _Db = db;
        }

        public async Task<IActionResult> Index()
        {
            var project = _Db.Projects;
            //var employer = await _userManager.GetUserAsync(User);
            //var project = _Db.Projects.Where(r => r.EmployerId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpGet]
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
            //var project = _Db.Projects.Where(r => r.EmployerId == employer.Id);
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            if (ModelState.IsValid)
            {
                project.CreateTime = DateTime.Now;
                project.EmployerId = (await _userManager.GetUserAsync(User)).Id;
                _Db.Projects.Add(project);
                _Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }
        [HttpGet]
        public IActionResult Detail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var project = _Db.Projects.Find(id);

            return View(project);
        }

        [HttpGet]
        public IActionResult Edit(int? projId)
        {            
            if(projId == null || projId == 0)
            {
                return NotFound();
            }
            var project = _Db.Projects.Find(projId);
            /*if (User.Identity.Name == project.EmployerId)
            {
                return View(project);
            }*/
            return View(project);
                
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public async Task<IActionResult> MakeBid(Bid bid)
        {
            if (ModelState.IsValid)
            {
                var employer = await _userManager.GetUserAsync(User);
                var projectid = bid.ProjectId;
                var project = _Db.Projects.Find(projectid);
                var projectbids = _Db.Entry(project)
                    .Collection(e => e.ProjectBids)
                    .Query()
                    .ToList();
                foreach(Bid bids in projectbids){
                    if (bids.FreelancerId.Equals(employer.Id))
                    {
                        return RedirectToAction("index", "home");
                    }
                }
                _Db.Bids.Add(bid);
                _Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bid);
        }
    }
}
