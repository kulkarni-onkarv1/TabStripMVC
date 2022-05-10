using System.Collections.Generic;
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
                throw;
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

        Task<CandidateAcademic> IRepository<CandidateAcademic, string>.UpdateAsync(CandidateAcademic entity, string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
