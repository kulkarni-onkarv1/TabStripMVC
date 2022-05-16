using System.ComponentModel.DataAnnotations;

namespace TabStripDemo.Models
{
    public class ServerSideValidationAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return false;
        }
    }
}
