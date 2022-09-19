using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Trial3.Areas.Identity.Data;
using Trial3.Models;
using System.Data.Entity;
using System.Security.Claims;

namespace Trial3.Controllers
{
    [Authorize]
    public class BidController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string Getuserid() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public BidController(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _Db = db;
            _httpContextAccessor = httpContextAccessor;
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
        public IActionResult MakeBid(Bid bid)
        {
            if (!ModelState.IsValid)
            {
                return View(bid);
            }
            var projectid = bid.ProjectId;
            Bid bids = _Db.Bids.FirstOrDefault(x => x.FreelancerId == Getuserid() && x.ProjectId == projectid);
            if (bids != null)
            {
                ViewBag.status = "You cant Bid on your own Project";
                return RedirectToAction("index", "home");
            }
            /*foreach (Bid bids in projectbids)
            {
                if (bids.FreelancerId == employer.Id)
                {
                    ViewBag.status = "You cant Bid on your own Project";
                    return RedirectToAction("index", "home");
                }
            }*/
            _Db.Bids.Add(bid);
            _Db.SaveChanges();
            ViewBag.mbstatus = true;
            return RedirectToAction("Index","project");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoles.Frellancer + ", " + UserRoles.Employer)]
        public IActionResult Acceptbid(int bidId)
        {
            if (bidId == 0)
            {
                return View("Error");
            }
            Bid bid = _Db.Bids.Find(bidId);
            if(bid == null)
            {
                return NotFound();
            }
            if (bid.status == "Accepted")
            {
                return RedirectToAction("index", "home");
            }
            bid.status = "Accepted";
            var projectid = bid.ProjectId;
            _Db.Bids.Update(bid);
            _Db.SaveChanges();
            Project project = _Db.Projects.Find(projectid);
            project.FreelancerId = bid.FreelancerId;
            project.Status = "Accepted";
            _Db.Projects.Update(project);
            _Db.SaveChanges();
            MessageBox messagebox = new MessageBox
            {
                FreelancerId = project.FreelancerId,
                EmployerId = project.EmployerId,
                ProjectId = project.projectId,
                Status = "Active",
                Title = "messageBox"
            };
            _Db.MessageBoxes.Add(messagebox);
            _Db.SaveChanges();
            return RedirectToAction("UserBids", "user");
        }
    }
}
