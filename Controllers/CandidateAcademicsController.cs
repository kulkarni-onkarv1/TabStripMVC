using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using TabStripDemo.Models;
using TabStripDemo.Repositories;
using TabStripDemo.SessionExtensions;

namespace TabStripDemo.Controllers
{
    
    public class CandidateAcademicsController : Controller
    {
        List<String> DegreeList;
        List<String> FieldList;
        List<string> PreferedLanguageList;
        Member members=new Member();
        private readonly IRepository<CandidateAcademic, string> candidateAcademicsAccess;
        private readonly CandidateAcademic candidateAcademics;
        private readonly IRepository<User, string> userAccess;

        public CandidateAcademicsController(IRepository<CandidateAcademic,string> candidateAcademicsAccess,CandidateAcademic candidateAcademics, IRepository<User, string> userAccess)
        {
            this.candidateAcademicsAccess = candidateAcademicsAccess;
            this.candidateAcademics = candidateAcademics;
            this.userAccess = userAccess;
            DegreeList= new List<String>()
            {
                "B.Tech","B.E","M.Tech","M.E","M.C.A","B.C.A","Msc.Comp"
            };
            FieldList = new List<String>()
            {
                "Computer Science and Engineering","Information Technology","Electrical Engineering","Mechanical Engineering","Electronics and Telecommunication","Electronics","Mechatronics"
            };
            PreferedLanguageList = new List<String>()
            {
                "Kotlin","C","C++","C Sharp","Perl","Java"
            };
        }

        [HttpPost]
        public IActionResult GetCandidateAcademics(RegisterUser registerUser)
        {
            if (!ModelState.IsValid)
            {
                TempData["InvalidCredentialsEntryErrorMessage"] = "Oh!Oh!Invalid Entries.Please Check All Inputs";               
                return RedirectToAction("Index", "Authentication");
            }
            HttpContext.Session.SetSessionData<RegisterUser>("UserCredentials", registerUser);
            ViewBag.Degree = new SelectList(DegreeList, "Degree");
            ViewBag.Field = new SelectList(FieldList,"Field");
            ViewBag.PreferedLanguageList = new SelectList(PreferedLanguageList, "PreferedLanguageList");
            return View(candidateAcademics);
        }
        
        [HttpPost]
        public IActionResult RegisterCandidate(CandidateAcademic candidateAcademic)
        {
            if (!ModelState.IsValid)
            {
                TempData["InvalidAcademicsEntryErrorMessage"] = "Oh!Oh!Invalid Entries.Please Check All Inputs";
                var registerUser = HttpContext.Session.GetSessionData<RegisterUser>("UserCredentials");
                return RedirectToAction("GetCandidateAcademics", new { registerUser = registerUser });
            }
            if (HttpContext.Session.GetSessionData<RegisterUser>("UserCredentials") == null)
            {
                return RedirectToAction("Index","Authentication");
            }
            HttpContext.Session.SetSessionData<CandidateAcademic>("CandidateAcademics", candidateAcademic);
            return RedirectToAction("ValidateMail");
        }
        public IActionResult ValidateMail()
        {
            var getUserCredentials = HttpContext.Session.GetSessionData<RegisterUser>("UserCredentials");
            if(getUserCredentials == null)
            {
                return RedirectToAction("Index", "Authentication");
            }
            var OTP = MailService.ValidateMailID(getUserCredentials);
            if (OTP == null)
            {
                TempData["RegisterResponseMessage"] = "Unable to register you at this moment!Please try again later!";
                return RedirectToAction("Index", "Authentication");
            }
            HttpContext.Session.SetString("OTP", OTP);
            return View(members);
        }       
        [HttpPost]
        public IActionResult ValidateMail(Member member)
        {
            if (member.OTP.ToString() == HttpContext.Session.GetString("OTP"))
            {
                var getUserCredentials = HttpContext.Session.GetSessionData<RegisterUser>("UserCredentials");
                var getCandidateAcademics= HttpContext.Session.GetSessionData<CandidateAcademic>("CandidateAcademics");
                var encryptedpAssword = EncryptorDecryptor.EncryptAsync(getUserCredentials.Password);
                var user = new User()
                {
                    MailId = getUserCredentials.MailID,
                    Password = encryptedpAssword,
                    LockOutStatus = 0
                };
                var createUser = userAccess.CreateAsync(user).Result;
                if(createUser == null)
                {
                    TempData["RegisterResponseMessage"] = "Unable to register you at this moment!Please try again later!";
                }
                else
                {
                    getCandidateAcademics.UserId = createUser.UserId;
                    var insertCandidateAcademics = candidateAcademicsAccess.CreateAsync(getCandidateAcademics).Result;
                    if (insertCandidateAcademics != null)
                    {
                        TempData["RegisterResponseMessage"] = "You have been registered successfully!";
                    }
                    else
                    {
                        TempData["RegisterResponseMessage"] = "Unable to register you at this moment!Please try again later!";
                    }
                }               
               
                return RedirectToAction("Index","Authentication");
            }
            else
            {
                TempData["InvalidOTPErrorMessage"] = "Oh!Oh! Invalid OTP.Please Enter Valid OTP!";
                return View(member);
            }
        }

        public JsonResult IsSixDigitNumber(int OTP)
        {
            if (OTP.ToString() == HttpContext.Session.GetString("OTP"))
            {
                return Json(data:true);
            }
            return Json(data:"Please enter valid 6 digit OTP");
        }
        [HttpGet]
        public IActionResult CandidateProfile(string EncrUderID)
        {
            return View();
        }
    }
}
