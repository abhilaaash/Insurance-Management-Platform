using EInsuranceProject.Model;
using EInsuranceProject.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace EInsuranceProject.Repository
{
    public interface ICustomerRepository: IEntityRepository<Customer>
    {
        public Task<List<Customer>> GetCustomersByAgentIdAsync(int agentId);
        public  Task<int> AddUser(User user);
        public  Task AddCustomerWithUser(Customer cust);
        public Task<List<Policy>> GetCustomerPoliciesAsync(int userId, Parameter filterParameter);
        public Customer GetByPhone(string username);
        
        public Customer GetByEmail(string username);
        
    }
}
