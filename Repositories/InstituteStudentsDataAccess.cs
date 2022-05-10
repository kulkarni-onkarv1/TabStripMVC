using System.Collections.Generic;
using System.Linq;
using TabStripDemo.Models;

namespace TabStripDemo.Repositories
{
    public class InstituteStudentsDataAccess
    {
        CollegeContext _collegeContext;
        public InstituteStudentsDataAccess()
        {
            _collegeContext = new CollegeContext();
        }
        public Student GetByEmail(string MailID)
        {
            var checkStudent = _collegeContext.Students.ToList().Where(M => M.InstituteMailId == MailID).FirstOrDefault();
            return checkStudent;
        }
    }
}
