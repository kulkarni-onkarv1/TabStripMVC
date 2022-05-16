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
        private readonly IRepository<User, string> userAccess;

        public CandidateParticipationController(IRepository<User, string> userAccess)
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
            this.userAccess = userAccess;
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
                /*foreach(var transaction in getcandidateTransactions)
                {
                    transaction.TransactionDate=Convert.ToDateTime(transaction.TransactionDate);
                }*/
            }            
            var candidateDetails = new CandidateDetails()
            {
                candidateTransactions = getcandidateTransactions,                
                UserId = EncrUserId,
                RoleID = RoleID
            };
            
            return View(candidateDetails);
        }

        public IActionResult GetTransactionRequest(string EncrUserID)
        {
            CandidateTransaction transaction = new CandidateTransaction();
            transaction.UserId=int.Parse(EncryptorDecryptor.DecryptAsync(EncrUserID));
            ViewBag.TransactionTypes = new SelectList(TransactionTypes, "TransactionTypes");
            return View(transaction);
        }
        [HttpPost]
        public IActionResult GetTransactionRequest(CandidateTransaction transaction)
        {
            if (!ModelState.IsValid)
            {
                TempData["InvalidTransactionEntryErrorMessage"] = "Oh!Oh!Invalid Entries.Please Check All Inputs";
                var EncrUserID = EncryptorDecryptor.EncryptAsync(transaction.UserId.ToString());
                return RedirectToAction("GetTransactionRequest", new { EncrUserID = EncrUserID });
            }
            var generatePaymentClaim = candidateParticipation.CreatePaymentRequest(transaction).Result;
            if (generatePaymentClaim == null)
            {
                TempData["TransactionRequestSubmissionResponseMessage"] = "Unable to process your request at this time.Please try again letter";
            }
            else
            {
                TempData["TransactionRequestSubmissionResponseMessage"] = "Request has been submitted successfully!You will be notified at status change";
            }
            return RedirectToAction("Index",new { EncrUserId = EncryptorDecryptor.EncryptAsync(transaction.UserId.ToString()) });
        }

        public IActionResult ResolveCandidateTransaction(int Urn)
        {
            var getCandidateTransaction= candidateParticipation.GetTransactions().Where(U=>U.Urn==Urn).FirstOrDefault();
            ViewBag.ResolveStatus= new SelectList(status, "Code", "StatusText");
            return View(getCandidateTransaction);
        }

        [HttpPost]
        public IActionResult ResolveCandidateTransaction(CandidateTransaction transaction)
        {
            if (!ModelState.IsValid)
            {
                TempData["InvalidTransactionResolutionEntryErrorMessage"] = "Oh!Oh!Invalid Entries.Please Check All Inputs";
                return RedirectToAction("ResolveCandidateTransaction", new { Urn = transaction.Urn });
            }
            candidateParticipation.StatusEvent += MailService.OnTransactionStatusChange;
            var CandidateMailID = userAccess.GetByUserIdAsync(transaction.UserId).Result.MailId;
            var resolvingTransaction=candidateParticipation.ResolveTransaction(transaction, CandidateMailID).Result;
            if (resolvingTransaction == null)
            {
                TempData["TransactionResolutionResponseMessage"] = "Unable to process your request at this time.Please try again letter";
            }
            else
            {
                TempData["TransactionResolutionResponseMessage"] = "Transaction has been resolved!";
            }
            var EncrUserId = EncryptorDecryptor.EncryptAsync(AuthenticationController.AdminUserID.ToString());
            return RedirectToAction("Index", new { EncrUserId = EncrUserId });
        }
        [HttpPost]
        public JsonResult IsValidAmount(double Amount)
        {
            if (Amount <= 0)
            {
                return Json(data: "Transaction amount cannot be zero or negative");
            }
            return Json(data: true);
        }
    }
}
