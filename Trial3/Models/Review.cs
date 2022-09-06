using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trial3.Areas.Identity.Data;

namespace Trial3.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string? UserReview { get; set; }
        [Range(0,5,ErrorMessage ="Star Should be between 0 to 5")]
        public int? Stars { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string? CreatorId { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("CreatorId")]
        public virtual ApplicationUser Creator { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
