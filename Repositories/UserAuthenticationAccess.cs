using Microsoft.EntityFrameworkCore;
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

        async Task<List<User>> IRepository<User, string>.GetAsync()
        {
            var GetUser=await _collegeContext.Users.ToListAsync();
            return GetUser;
        }

        async Task<User> IRepository<User, string>.GetByEmailAsync(string MailID)
        {
            try
            {
                var GetUser = await _collegeContext.Users.ToListAsync();
                var user = GetUser.Where(M => M.MailId == MailID).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                else
                {
                    return user;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        async Task<User> IRepository<User, string>.GetByUserIdAsync(int? id)
        {
            var GetUser = await _collegeContext.Users.FindAsync(id);
            return GetUser;
        }

        Task<User> IRepository<User, string>.UpdateAsync(User entity, string id)
        {
            throw new System.NotImplementedException();
        }

        public int? GetRoleByUserId(int UserID)
        {
            var UserRole = _collegeContext.UserRoles.Where(U => U.UserId == UserID).FirstOrDefault();
			if (UserRole == null)
			{
                return null;
			}
            return UserRole.RoleId;
        }
    }
}
