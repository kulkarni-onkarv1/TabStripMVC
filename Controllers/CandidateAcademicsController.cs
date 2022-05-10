using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabStripDemo.Models;
using TabStripDemo.Repositories;
using TabStripDemo.SessionExtensions;

namespace TabStripDemo.Controllers
{
    public class CandidateAcademicsController : Controller
    {
        Member members=new Member();
        private readonly IRepository<CandidateAcademic, string> candidateAcademicsAccess;
        private readonly CandidateAcademic candidateAcademics;
        private readonly IRepository<User, string> userAccess;

        public CandidateAcademicsController(IRepository<CandidateAcademic,string> candidateAcademicsAccess,CandidateAcademic candidateAcademics, IRepository<User, string> userAccess)
        {
            this.candidateAcademicsAccess = candidateAcademicsAccess;
            this.candidateAcademics = candidateAcademics;
            this.userAccess = userAccess;
        }

        public IActionResult Create(RegisterUser registerUser)
        {
            HttpContext.Session.SetSessionData<RegisterUser>("UserCredentials", registerUser);
            return View(candidateAcademics);
        }
        [HttpPost]
        public IActionResult Create(CandidateAcademic candidateAcademic)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetSessionData<CandidateAcademic>("CandidateAcademics", candidateAcademic);               
                return RedirectToAction("ValidateMail");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }
        public IActionResult ValidateMail()
        {
            var getUserCredentials = HttpContext.Session.GetSessionData<RegisterUser>("UserCredentials");
            var OTP = MailService.ValidateMailID(getUserCredentials);
            HttpContext.Session.SetString("OTP", OTP);
            return View(members);
        }       
        [HttpPost]
        public IActionResult ValidateMail(Member member)
        {
            if (member.OTP == HttpContext.Session.GetString("OTP"))
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
                getCandidateAcademics.UserId= createUser.UserId;
                var insertCandidateAcademics = candidateAcademicsAccess.CreateAsync(getCandidateAcademics);
                var responseMessage = EncryptorDecryptor.EncryptAsync("You have You have been registered successfully!");
                return RedirectToAction("Index","Authentication",new { ResponseMessage = responseMessage });
            }
            else
            {
                return View("ValidateMail");
            }
        }
    }
}
