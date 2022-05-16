using System.ComponentModel.DataAnnotations;

namespace TabStripDemo.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage ="Enter your registered Mail ID to log in")]
        [EmailAddress(ErrorMessage = "Invalid Email! Please check!")]
        public string MailID { get; set;}
        [Required(ErrorMessage ="Enter password to log in")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Invalid Password!Please check")]
        public string Password { get; set;} 
    }
}
