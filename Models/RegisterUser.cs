using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TabStripDemo.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "Mail Id is mandatory field")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Remote(action:"IsUserNameAvailable", controller:"Authentication",HttpMethod ="POST")]
        public string MailID { get; set; }

        [Required(ErrorMessage ="Mandatory Field")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Password")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }

        //[Remote(action: "CheckPasswordMatch", controller: "Authentication", HttpMethod = "POST", ErrorMessage = "Password and Confirm Password Does Not Match")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
    }
}
