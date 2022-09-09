using System.ComponentModel.DataAnnotations;
using Trial3.Areas.Identity.Data;

namespace Trial3.Models
{
    public class MessageBox
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string EmployerId { get; set; }
        public string FreelancerId { get; set; }
        public int ProjectId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public List<Messages> Messages { get; set; }
        public string? Status { get; set; }
        public virtual ApplicationUser? Employer { get; set; }
        public virtual ApplicationUser? Freelancer { get; set; }
        public virtual Project? Project { get; set; }
    }
}
