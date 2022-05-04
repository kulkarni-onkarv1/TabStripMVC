using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TabStripDemo.Models;

namespace TabStripDemo.Controllers
{
    public class AuthenticationController : Controller
    {
        Member members = new Member();
        public AuthenticationController()
        {          
           
        }
        public IActionResult Index()
        {
            //List<Member> members = new List<Member>();            
            return View(members);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLogin userLogin)
        {
            return RedirectToAction("Login");
        }
    }
}
