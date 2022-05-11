using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TabStripDemo.Models
{
    public class Member
    {
        public UserLogin UserLogin { get; set; }
        public RegisterUser RegisterUser { get; set; }
        [Required]
        //[Remote(action: "IsValidOTP", controller: "Authentication", HttpMethod = "POST")]
        public string OTP { get; set; }
    }
}
