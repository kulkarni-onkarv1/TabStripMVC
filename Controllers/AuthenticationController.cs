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
        public static int AdminUserID=10013;
        public static bool IsAuthenticated { get; set; } = false;
        private readonly IRepository<User, string> userAccess;

        public AuthenticationController(IRepository<User,string> userAccess)
        {
            this.userAccess = userAccess;
        }
        
        public IActionResult Index()
        {       
            return View("Index",members);
        }
        [HttpPost]
        public IActionResult Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                TempData["InvalidLoginCredentialsErrorMessage"] = $"Oh!Oh!Invalid Credentials!";
                return RedirectToAction("Index", "Authentication");
            }
            var getUser = userAccess.GetByEmailAsync(userLogin.MailID).Result;
            
            if ( getUser== null)
            {
                TempData["InvalidLoginCredentialsErrorMessage"] = $"No User Exists With Entered Mail ID {userLogin.MailID}";               
                return RedirectToAction("Index", "Authentication");
            }
            else if(userLogin.Password!= EncryptorDecryptor.DecryptAsync(getUser.Password))
            {
                TempData["InvalidLoginCredentialsErrorMessage"] = $"Oh!Oh!Invalid Password!";
                return RedirectToAction("Index", "Authentication");
            }
            //var decryptedPassword = EncryptorDecryptor.DecryptAsync(userLogin.Password);
            // return LocalRedirect("/");
            IsAuthenticated = true;       
            var EncrUserId = EncryptorDecryptor.EncryptAsync($"{getUser.UserId.ToString()}");
            return RedirectToAction("Index", "CandidateParticipation",new { EncrUserId = EncrUserId });
        }
        

        public IActionResult Logout()
        {
            IsAuthenticated = false;
            return RedirectToAction("Index");
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
