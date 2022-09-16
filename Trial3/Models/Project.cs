using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trial3.Areas.Identity.Data;

namespace Trial3.Models
{
    public class Project
    {
        [Key]
        public int projectId { get; set; }
        [Required]
        [StringLength(100)]
        public string ProjectName { get; set; }
        [Required]
        [StringLength(10000, MinimumLength = 250, ErrorMessage = "Remark must have min length of 10 and max Length of 50")]
        public string Description { get; set; }
        public string Status { get; set; }
        [Required]
        public string? tags { get; set; }
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        public string? EmployerId { get; set; }
        public string? FreelancerId { get; set; }
        public int? TotalBids { get; set; }
        public int? MinBid { get; set; }
        public int? MaxBid { get; set; }
        public int? AvgBid { get; set; }
        public List<Bid>? ProjectBids { get; set; }
        public virtual ApplicationUser? Employer { get; set; }
        public virtual ApplicationUser? Freelancer { get; set; }
    }
}
