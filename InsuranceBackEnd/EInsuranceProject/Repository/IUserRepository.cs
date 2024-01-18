using EInsuranceProject.Model;
using Microsoft.EntityFrameworkCore;

namespace EInsuranceProject.Repository
{
    public interface IUserRepository:IEntityRepository<User>
    {
        public string GetRoleNameForUser(string username);
        public int GetRoleIdForUser(string username);
        public int GetUserIdForUser(string username);
        public User GetUserByUsername(string username);
        public void UpdatePassword(User user, string newPassword);

        public Task<User> FindByUserNameAsync(string Username);
    }
}
