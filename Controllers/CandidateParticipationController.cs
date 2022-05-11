using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TabStripDemo.Models;
using TabStripDemo.Repositories;

namespace TabStripDemo.Controllers
{
    [Authorize]
    public class CandidateParticipationController : Controller
    {
        CandidateParticipation candidateParticipation;
        UserAuthenticationAccess userAuthenticationAccess;
        List<CandidateTransaction> getcandidateTransactions;

        public CandidateParticipationController()
        {
            candidateParticipation= new CandidateParticipation();
            userAuthenticationAccess= new UserAuthenticationAccess();
        }
        public IActionResult Index(string EncrUserId)
        {
            var DecrUserId=EncryptorDecryptor.DecryptAsync(EncrUserId);
            if (DecrUserId == null)
            {
                return LocalRedirect("/");
            }
            var RoleID = userAuthenticationAccess.GetRoleByUserId(int.Parse(DecrUserId));
            if(RoleID != null)
			{
                getcandidateTransactions = candidateParticipation.GetTransactions();
			}
			else
			{
                getcandidateTransactions = candidateParticipation.GetByUserId(int.Parse(DecrUserId));
            }            
            var candidateDetails = new CandidateDetails()
            {
                candidateTransactions = getcandidateTransactions,
                UserId = EncrUserId,
                RoleID = RoleID

            };
            /*candidateDetails.user=new User()
            {
                UserId= int.Parse(DecrUserId),
            };*/
            return View(candidateDetails);
        }

        public IActionResult Create(string id)
        {
            CandidateTransaction transaction = new CandidateTransaction();
            transaction.UserId=int.Parse(EncryptorDecryptor.DecryptAsync(id));
            //
            return View(transaction);
        }
        [HttpPost]
        public IActionResult Create(CandidateTransaction transaction)
        {
            var generatePaymentClaim = candidateParticipation.CreatePaymentRequest(transaction);
            return RedirectToAction("Index",new { EncrUserId = EncryptorDecryptor.EncryptAsync(transaction.UserId.ToString()) });
        }
    }
}
