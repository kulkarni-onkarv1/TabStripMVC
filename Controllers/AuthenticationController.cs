using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TabStripDemo.Models;
using TabStripDemo.Repositories;
using TabStripDemo.SessionExtensions;

namespace TabStripDemo.Controllers
{
    public class AuthenticationController : Controller
    {
        Member members = new Member();
        UserLogin UserLogin=new UserLogin();
        RegisterUser RegisterUser=new RegisterUser();
        InstituteStudentsDataAccess instituteStudentsDataAccess;
        private readonly IRepository<User, string> userAccess;

        public AuthenticationController(IRepository<User,string> userAccess)
        {
            this.userAccess = userAccess;
            instituteStudentsDataAccess = new InstituteStudentsDataAccess();
        }
        public IActionResult Index(String ResponseMessage)
        {
            //List<Member> members = new List<Member>();
            //ViewBag.Message = HttpContext.Session.GetString("InvalidCredentials");
            if (ResponseMessage != null)
            {
                ViewBag.Message = EncryptorDecryptor.DecryptAsync(ResponseMessage);
            }         
            return View("Index",members);
            //return View("_RegisterPartial", RegisterUser);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLogin userLogin)
        {
            var getUser = userAccess.GetByEmailAsync(userLogin.MailID).Result;
            if ( getUser== null)
            {
                var ErrorMessage = EncryptorDecryptor.EncryptAsync($"No User Exists With Entered Mail ID {userLogin.MailID}");
                return RedirectToAction("Index", "Authentication", new { ResponseMessage =  ErrorMessage});
            }
            else if(userLogin.Password!= EncryptorDecryptor.DecryptAsync(getUser.Password))
            {
                var ErrorMessage = EncryptorDecryptor.EncryptAsync("Oh Oh! Invalid Password");
                return RedirectToAction("Index", "Authentication", new { ResponseMessage = ErrorMessage });
            }
            //var decryptedPassword = EncryptorDecryptor.DecryptAsync(userLogin.Password);
            
            return RedirectToAction("Login");
        }

        [HttpPost]
        public JsonResult IsUserNameAvailable(string MailID)
        {
            var getUser = userAccess.GetByEmailAsync(MailID).Result;            
            if(getUser != null)
            {
                return Json(data: "Mail ID is Already Registered");
            }
            /*var getStudent = instituteStudentsDataAccess.GetByEmail(MailID);
            if (getStudent == null)
            {
                return Json(data: "Enter Your Institute Mail ID Only");
            }*/
            return Json(data: true);
        }

    }
}
