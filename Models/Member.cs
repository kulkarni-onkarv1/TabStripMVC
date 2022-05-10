using System.ComponentModel.DataAnnotations;

namespace TabStripDemo.Models
{
    public class Member
    {
        public UserLogin UserLogin { get; set; }
        public RegisterUser RegisterUser { get; set; }
        [Required]
        public string OTP { get; set; }
    }
}
