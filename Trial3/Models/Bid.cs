using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trial3.Areas.Identity.Data;

namespace Trial3.Models
{
    public class Bid
    {
        [Key]
        public int BidId { get; set; }
        [Required]
        public int BidPrice { get; set; }
        [Required]
        [StringLength(100)]
        public string? Discription { get; set; }
        [Required]
        public string status { get; set; }
        public string? FreelancerId { get; set; }
        [ForeignKey ("FreelancerId")]
        public virtual ApplicationUser? Freelancer { get; set; }
        public int? ProjectId { get; set; }
        [ForeignKey ("ProjectId")]
        public virtual Project? Project { get; set; }


    }
}
