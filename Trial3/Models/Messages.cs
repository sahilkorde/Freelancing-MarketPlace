using Trial3.Areas.Identity.Data;

namespace Trial3.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int MessageBoxId { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public string SenderId { get; set; }
        public string Status { get; set; }
        public virtual ApplicationUser? Sender { get; set; }
        public virtual MessageBox? MessageBox { get; set; }
    }
}
