using System;
using System.Collections.Generic;

namespace TabStripDemo.Models
{
    public partial class CandidateAcademic
    {
        public int Urn { get; set; }
        public int? UserId { get; set; }
        public string CollegeName { get; set; }
        public string Degree { get; set; }
        public string Branch { get; set; }
        public string Pglanganguage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual User User { get; set; }
    }
}
