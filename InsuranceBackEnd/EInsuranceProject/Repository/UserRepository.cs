using EInsuranceProject.Data;
using EInsuranceProject.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace EInsuranceProject.Repository
{
    public class UserRepository:EntityRepository<User>,IUserRepository
    {
        private readonly InsuranceContext _context;
        public UserRepository(InsuranceContext context) : base(context) 
        {
            _context = context;
        }

        public string GetRoleNameForUser(string username)
        {
          //  int? roleId = _context.Users.Where(u=> u.Username == username).Select(r=>r.RoleId).FirstOrDefault();
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user != null && user.RoleId != null)
            {
               // return  (int) roleId;
                return  GetRoleNameByRoleId(user.RoleId);
            }

            return null;
        }
        public int GetRoleIdForUser(string username)
        {
            int? roleId = _context.Users.Where(u => u.Username == username).Select(r => r.RoleId).FirstOrDefault();
          //  var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (roleId !=null)
            {
                return (int)roleId;
                // return  GetRoleNameByRoleId(user.RoleId);
            }

            return 0;
        }
        public int GetUserIdForUser(string username)
        {
            int? roleId = _context.Users.Where(u => u.Username == username).Select(r => r.UserId).FirstOrDefault();
            //  var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (roleId != null)
            {
                return (int)roleId;
                // return  GetRoleNameByRoleId(user.RoleId);
            }

            return 0;
        }

        private string GetRoleNameByRoleId(int? roleId)
        {
            var roleName = _context.Roles
                .Where(r => r.RoleId == roleId)
                .Select(r => r.RoleName)
                .FirstOrDefault();

            return roleName;
        }


        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public void UpdatePassword(User user, string newPassword)
        {
            if (user != null)
            {
                user.Paasword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                _context.SaveChanges();
            }
        }
//---------------------------------------------------------------------------------------
        public async Task<User> FindByUserNameAsync(string Username)
        {
            return await _context.Users.Where(user => EF.Functions.Collate(user.Username, "SQL_Latin1_General_CP1_CS_AS") == Username).FirstOrDefaultAsync();
        }

    }
}
