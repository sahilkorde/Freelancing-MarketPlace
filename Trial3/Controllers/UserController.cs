using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trial3.Areas.Identity.Data;
using Trial3.Models;

namespace Trial3.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _Db;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _Db = db;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> UserProject()
        {
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
        public async Task<IActionResult> UserBids()
        {
            var employer = await _userManager.GetUserAsync(User);
            var userBids = _Db.Entry(employer)
                .Collection(e => e.Bids)
                .Query()
                .ToList();
            return View(userBids);
        }
        public async Task<IActionResult> Acceptbid(int bidId)
        {
            if (bidId == 0 || bidId == null)
            {
                return View("Error");
            }
            Bid bid = _Db.Bids.Find(bidId);
            if(bid.status == "Accepted")
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
            return RedirectToAction("UserBids");
        }

        [HttpGet]
        public async Task<IActionResult> Chats(int? messageBoxId)
        {

            var employer = await _userManager.GetUserAsync(User);
            List<MessageBox>? EmessageBoxes = _Db.Entry(employer)
                .Collection(e => e.EmployerMessageBoxes)
                .Query()
                .ToList();
            List<MessageBox>? FmessageBoxes = _Db.Entry(employer)
                .Collection(e => e.FreelacerMessageBoxes)
                .Query()
                .Where(p => p.Status =="Active")
                .ToList();
            var user = _Db.Users
                .Include(x => x.EmployerMessageBoxes)
                .Include(x => x.FreelacerMessageBoxes)
                .FirstOrDefault(x => x.Id == employer.Id);
            MessageBox messagebox = _Db.MessageBoxes
                .Include(c => c.Messages)
                .FirstOrDefault(x => x.Id == messageBoxId);
            
            List<MessageBox>? messageBoxes1 = new();
            messageBoxes1 = EmessageBoxes.ToList();
            foreach (var i in FmessageBoxes)
            {
                messageBoxes1.Add(i);
            }
            ChatView messageboxandmessages = new ChatView
            {
                messageBoxes = messageBoxes1,
                messageBox = messagebox
            };
            return View(messageboxandmessages);
        }

        public async Task<IActionResult> CreateMessage(String Message, int messageid)
        {
            var sender = await _userManager.GetUserAsync(User);
            Messages message = new Messages
            {
                Message = Message,
                MessageBoxId = messageid,
                SenderId = sender.Id,
                CreateTime = DateTime.Now,
                Status = "Send"
            };
            _Db.Messages.Add(message);
            _Db.SaveChanges();
            return RedirectToAction("Chats", new { messageBoxId = messageid});
        }
    }
}
