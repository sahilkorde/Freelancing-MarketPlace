namespace Trial3.Models
{
    public class ProjectBidView
    {
        public Project Projects { get; set; }
        public IEnumerable<Bid>? Bids { get; set; }
    }
}
