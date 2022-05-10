using System;
using System.Collections.Generic;

namespace TabStripDemo.Models
{
    public partial class User
    {
        public User()
        {
            CandidateAcademics = new HashSet<CandidateAcademic>();
            CandidateTransactions = new HashSet<CandidateTransaction>();
            UserRoles = new HashSet<UserRole>();
        }

        public int UserId { get; set; }
        public string MailId { get; set; }
        public string Password { get; set; }
        public int LockOutStatus { get; set; }

        public virtual ICollection<CandidateAcademic> CandidateAcademics { get; set; }
        public virtual ICollection<CandidateTransaction> CandidateTransactions { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
