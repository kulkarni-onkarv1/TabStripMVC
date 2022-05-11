using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TabStripDemo.Models
{
    public partial class CandidateAcademic
    {
        public int Urn { get; set; }
        public int? UserId { get; set; }
        [Required(ErrorMessage ="Mandatory")]
        public string CollegeName { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public string Degree { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public string Branch { get; set; }

        public virtual User User { get; set; }
    }
}
