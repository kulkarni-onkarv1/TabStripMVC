using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TabStripDemo.Models
{
    public partial class CandidateAcademic
    {
        public int Urn { get; set; }
        public int? UserId { get; set; }
        [Required(ErrorMessage ="Please mention college name")]
        public string CollegeName { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string Branch { get; set; }
        [Required]
        public string Pglanganguage { get; set; }
        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }

        public virtual User User { get; set; }
    }
}
