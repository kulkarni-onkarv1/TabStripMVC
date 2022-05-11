using System.Collections.Generic;

namespace TabStripDemo.Models
{
    public class CandidateDetails
    {
        public List<CandidateTransaction> candidateTransactions { get; set; }
        public User user { get; set; }
        public CandidateAcademic candidateAcademic { get; set; }
        public string UserId { get; set; }
        public int? RoleID { get; set; }
    }
}
