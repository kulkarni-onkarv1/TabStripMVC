using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TabStripDemo.Models;
using TabStripDemo.Repositories;

namespace TabStripDemo.Controllers
{
    public class RequestStatusController : Controller
    {
        List<CandidateTransaction> getcandidateTransactions;
        CandidateParticipation candidateParticipation;
        public RequestStatusController()
        {
            candidateParticipation = new CandidateParticipation();
        }
        public IActionResult ResolvedTransactions(int status)
        {
            getcandidateTransactions=candidateParticipation.GetTransactions().Where(S=>S.ApprovalStatus== status).ToList();         
            return View(getcandidateTransactions);
        }       
    }
}
