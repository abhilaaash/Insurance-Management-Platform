using EInsuranceProject.Model;

namespace EInsuranceProject.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetAll(string[] innerTables);
        public Task<User> GetById(int Id);
        public Task AddUser(User Response);
        public Task<bool> UpdateUser(User Response);
        public Task<bool> DeleteUser(int id);
        public string GetRoleNameForUser(string username);
        public User FindUserAsync(Func<User, bool> predicate);
        public int GetRoleIdForUser(string username);
        public int GetUserIdForUser(string username);
        public User GetUserByUsername(string username);

        public bool UpdatePassword(string username, string newPassword);

        public  Task<User> FindUser(string username);


    }
}
