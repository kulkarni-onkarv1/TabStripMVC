using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabStripDemo.Models;

namespace TabStripDemo.Repositories
{
    public class CandidateDataAccess : IRepository<CandidateAcademic, string>
    {
        CollegeContext collegeContext;
        
        public CandidateDataAccess()
        {
            collegeContext = new CollegeContext();
        }
        async Task<CandidateAcademic> IRepository<CandidateAcademic, string>.CreateAsync(CandidateAcademic entity)
        {
            //return null;
            try
            {
                var insertCandidateAcademics = await collegeContext.CandidateAcademics.AddAsync(entity);
                await collegeContext.SaveChangesAsync();
                return insertCandidateAcademics.Entity;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        Task<CandidateAcademic> IRepository<CandidateAcademic, string>.DeleteAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        Task<List<CandidateAcademic>> IRepository<CandidateAcademic, string>.GetAsync()
        {
            throw new System.NotImplementedException();
        }

        Task<CandidateAcademic> IRepository<CandidateAcademic, string>.GetByEmailAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        Task<CandidateAcademic> IRepository<CandidateAcademic, string>.GetByUserIdAsync(int? id)
        {
            throw new System.NotImplementedException();
        }

        Task<CandidateAcademic> IRepository<CandidateAcademic, string>.UpdateAsync(CandidateAcademic entity, string id)
        {
            throw new System.NotImplementedException();
        }
    }

    public class CandidateParticipation
    {
        CollegeContext collegeContext;
        public delegate void StatusEventHandler(object source, EventArgs e, String MailID);
        public event StatusEventHandler? StatusEvent;
        public CandidateParticipation()
        {
            collegeContext=new CollegeContext();
        }

        public List<CandidateTransaction> GetTransactions()
		{
            return collegeContext.CandidateTransactions.ToList();
		}

        public List<CandidateTransaction> GetByUserId(int userId)
        {
            try
            {
                var getPaymentDetails = collegeContext.CandidateTransactions.ToList().Where(M => M.UserId == userId).ToList();
                return getPaymentDetails;
            }
            catch (System.Exception ex)
            {

                return null;
            }
        }

        public async Task<CandidateTransaction> CreatePaymentRequest(CandidateTransaction candidateTransaction)
        {
            //return null;
            try
            {
                var generatePaymentClaim = collegeContext.CandidateTransactions.Add(candidateTransaction);
                await collegeContext.SaveChangesAsync();
                return generatePaymentClaim.Entity;
            }
            catch (System.Exception ex)
            {

                return null;
            }
        }

        public async Task<CandidateTransaction> ResolveTransaction(CandidateTransaction transaction,String MailID)
        {
            var findTransaction = collegeContext.CandidateTransactions.Find(transaction.Urn);
            if (findTransaction != null)
            {
                findTransaction.Urn= transaction.Urn;
                findTransaction.UserId= transaction.UserId;
                findTransaction.TransactionType= transaction.TransactionType;
                findTransaction.ReciepientBank= transaction.ReciepientBank;
                findTransaction.Amount= transaction.Amount;
                findTransaction.Utr= transaction.Utr;
                findTransaction.TransactionRecieptPath=transaction.TransactionRecieptPath;
                findTransaction.ApprovalStatus=transaction.ApprovalStatus;
                findTransaction.ApproverRemark=transaction.ApproverRemark;
                findTransaction.ResponseDateTime = System.DateTime.Now;
            }          
            var statusChange=collegeContext.SaveChangesAsync().Result;
            if (statusChange==1)
            {
                if (StatusEvent != null)
                {
                    StatusEvent(this, EventArgs.Empty, MailID);
                }
                // Mail?.Invoke(this, EventArgs.Empty, MailID);
            }
            return findTransaction;
        }

    }
}
