using System.ComponentModel.DataAnnotations;

namespace TabStripDemo.Models
{
    public class UserLogin
    {
        [Required]
        public string MailID { get; set;}
        [Required]
        public string Password { get; set;} 
    }
}
