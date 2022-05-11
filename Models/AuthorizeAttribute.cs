using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using TabStripDemo.Controllers;

namespace TabStripDemo.Models
{
    public class AuthorizeAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!AuthenticationController.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Index", "Authentication", null);
            }
        }
    }
}
