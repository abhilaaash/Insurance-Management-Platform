using EInsuranceProject.DTO;
using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using EInsuranceProject.Repository;
using FinalProjectInsurance.DTO;
using System.Linq.Expressions;

namespace EInsuranceProject.Services
{
    public class UserService:IUserService
    {
        private IEntityRepository<User> _entityRepository;
        private IUserRepository _userRepository;
        public UserService(IEntityRepository<User> entityRepository, IUserRepository userRepository)
        {
            _entityRepository = entityRepository;
            _userRepository = userRepository;
        }
        public async Task AddUser(User response)
        {
            await _entityRepository.Insert(response);
        }

        public async Task<bool> DeleteUser(int id)
        {
            var ResponseToDelete = await _entityRepository.GetById(id);
            if (ResponseToDelete != null)
                return await _entityRepository.Delete(id);
            throw new Exception("No Feedback Found To Delete");
        }

        public async Task<IEnumerable<User>> GetAll(string[] innerTables)
        {
            Expression<Func<User, bool>> filterPredicate = e => e.Username != null;
            var responses = await _entityRepository.GetAll(innerTables, filterPredicate);
            if (responses.Count() > 0)
            {
                return responses;
            }
            throw new Exception("No User found");

        }


        public async Task<User> GetById(int Id)
        {
            var response = await _entityRepository.GetById(Id);
            if (response != null)
                return response;
            throw new Exception("No User Found");
        }

        public async Task<bool> UpdateUser(User response)
        {
            var UserToUpdate = await _entityRepository.GetById(response.UserId);
            if (UserToUpdate != null)
                return await _entityRepository.Update(response, response.UserId);
            throw new Exception("No User Found To Update");
        }
        public string GetRoleNameForUser(string username)
        {
            var roleName = _userRepository.GetRoleNameForUser(username);
           
                return roleName;

          //  throw new Exception("No user found ");
        }
        public int GetRoleIdForUser(string username)
        {
            var roleId = _userRepository.GetRoleIdForUser(username);
            if (roleId != null)
                return roleId;
            throw new Exception("No user found ");
            
        }
        public int GetUserIdForUser(string username)
        {
            var userId= _userRepository.GetUserIdForUser(username);
            if (userId != null)
                return userId;
            throw new Exception("No user found ");
        }
        public User FindUserAsync(Func<User, bool> predicate)
        {
            var user = _userRepository.FindUser(predicate);
            if (user != null)
            {
                return user;
            }
            throw new DataNotFoundExcpetion("No User Data found");

        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }

        public bool UpdatePassword(string username, string newPassword)
        {
            var user = _userRepository.GetUserByUsername(username);

            if (user != null)
            {
                _userRepository.UpdatePassword(user, newPassword);
                return true;
            }

            return false;
        }
        //------------------------------------------------------------------
        public async Task<User> FindUser(string username)
        {
            return await _userRepository.FindByUserNameAsync(username);
        }
    }
}
