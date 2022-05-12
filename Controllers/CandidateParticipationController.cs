using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
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
        List<Status> status;
        List<String> TransactionTypes;

        public CandidateParticipationController()
        {
            candidateParticipation= new CandidateParticipation();
            userAuthenticationAccess= new UserAuthenticationAccess();
            status = new List<Status>() { 
                new Status(){Code=1,StatusText="Approve"},
                new Status(){Code=2,StatusText="Reject" }
            };
            TransactionTypes = new List<string>()
            {
                "IMPS","NEFT","UPI","Third Party Within Bank/FT"
            };
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
            
            return View(candidateDetails);
        }

        public IActionResult Create(string id)
        {
            CandidateTransaction transaction = new CandidateTransaction();
            transaction.UserId=int.Parse(EncryptorDecryptor.DecryptAsync(id));
            ViewBag.TransactionTypes = new SelectList(TransactionTypes, "TransactionTypes");
            return View(transaction);
        }
        [HttpPost]
        public IActionResult Create(CandidateTransaction transaction)
        {
            var generatePaymentClaim = candidateParticipation.CreatePaymentRequest(transaction);
            return RedirectToAction("Index",new { EncrUserId = EncryptorDecryptor.EncryptAsync(transaction.UserId.ToString()) });
        }

        public IActionResult Resolve(int Urn)
        {
            var getCandidateTransaction= candidateParticipation.GetTransactions().Where(U=>U.Urn==Urn).FirstOrDefault();
            ViewBag.ResolveStatus= new SelectList(status, "Code", "StatusText");
            return View(getCandidateTransaction);
        }

        [HttpPost]
        public IActionResult Resolve(CandidateTransaction transaction)
        {
            var resolvingTransaction=candidateParticipation.ResolveTransaction(transaction);
            var EncrUserId = EncryptorDecryptor.EncryptAsync(AuthenticationController.AdminUserID.ToString());
            return RedirectToAction("Index", new { EncrUserId = EncrUserId });
        }
    }
}
