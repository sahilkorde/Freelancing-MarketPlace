using Trial3.Areas.Identity.Data;

namespace Trial3.Models
{
    public class UserView
    {
        public ApplicationUser user { get; set; }
        public int userProject { get; set; }
        public int userbids { get; set; }
    }
}
