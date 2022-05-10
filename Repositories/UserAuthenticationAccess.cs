using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabStripDemo.Models;

namespace TabStripDemo.Repositories
{
    public class UserAuthenticationAccess : IRepository<User, string>
    {
        CollegeContext _collegeContext;
       public UserAuthenticationAccess()
        {
            _collegeContext=new CollegeContext();
        }
        async Task<User> IRepository<User, string>.CreateAsync(User entity)
        {
            try
            {
                var registerUser = _collegeContext.Users.Add(entity);
                await _collegeContext.SaveChangesAsync();
                return registerUser.Entity;
            }
            catch(Exception ex)
            {
                return null;
            }
            
            //return ref registerUser;
            
        }

        Task<User> IRepository<User, string>.DeleteAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        Task<List<User>> IRepository<User, string>.GetAsync()
        {
            throw new System.NotImplementedException();
        }

        async Task<User> IRepository<User, string>.GetByEmailAsync(string MailID)
        {
            var user = _collegeContext.Users.ToList().Where(M=>M.MailId==MailID).FirstOrDefault();
            if (user==null)
            {
                return null;
            }
            else
            {
                return user;
            }
            //throw new System.NotImplementedException();
        }

        Task<User> IRepository<User, string>.UpdateAsync(User entity, string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
