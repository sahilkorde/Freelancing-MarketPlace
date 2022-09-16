using Microsoft.AspNetCore.Mvc;
using Trial3.Hubs;
using Microsoft.AspNetCore.SignalR;
using Trial3.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Trial3.Models;

namespace Trial3.Controllers
{
    [Route("[Controller]")]
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _chat;
        private readonly ApplicationDbContext _Db;

        private readonly UserManager<ApplicationUser> _userManager;
        public ChatController( IHubContext<ChatHub> chat, ApplicationDbContext Db,UserManager<ApplicationUser> userManager)
        {
            _chat = chat;
            _Db = Db;
            _userManager = userManager;
        }

        [HttpPost ("[action]/{connectionid}/{chatId}")]
        public async Task<IActionResult> JoinChat(string connectionid, int chatId)
        {
            string chatidstring = chatId.ToString();
            await _chat.Groups.AddToGroupAsync(connectionid, chatidstring);
            return Ok();
        }

        [HttpPost("[action]/{connectionid}/{chatId}")]
        public async Task<IActionResult> LeavRoom(string connectionid, int chatId)
        {
            string chatidstring = chatId.ToString();
            _chat.Groups.RemoveFromGroupAsync(connectionid, chatidstring);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(string Message, int chatId)
        {
            var sender = await _userManager.GetUserAsync(User);
            Messages message = new Messages
            {
                Message = Message,
                MessageBoxId = chatId,
                SenderId = sender.Id,
                CreateTime = DateTime.Now,
                Status = "Send"
            };
            _Db.Messages.Add(message);
            _Db.SaveChanges();

            var messageview = new
            {
                Msg = Message,
                sender = sender.Email,
                time = message.CreateTime.ToString("dd/MM/yyyy hh:mm:ss")
            };

            string chatidstring = chatId.ToString();
            await _chat.Clients.Group(chatidstring).SendAsync("recieveMessage", messageview);
            return Ok();
        }
    }
}
