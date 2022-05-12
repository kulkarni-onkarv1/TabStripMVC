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

        Task<CandidateAcademic> IRepository<CandidateAcademic, string>.GetByUserIdAsync(int id)
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

                throw;
            }
        }

        public async Task<CandidateTransaction> CreatePaymentRequest(CandidateTransaction candidateTransaction)
        {
            var generatePaymentClaim=collegeContext.CandidateTransactions.Add(candidateTransaction);
            await collegeContext.SaveChangesAsync();
            return generatePaymentClaim.Entity;
        }

        public async Task<CandidateTransaction> ResolveTransaction(CandidateTransaction transaction)
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
            collegeContext.SaveChangesAsync();
            return findTransaction;
        }

    }
}
