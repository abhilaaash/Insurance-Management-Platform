using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using EInsuranceProject.Wrapper;
using System.Linq.Expressions;

namespace EInsuranceProject.Services
{
    public interface ICustomerService
    {
        public Task<IEnumerable<Customer>> GetAll(string[] innerTables);
        public Task<Customer> GetById(int Id);
        public Task AddCustomer(Customer customer);
        public Task<bool> UpdateCustomer(Customer customer);
        public Task<bool> DeleteCustomer(int id);
        public Task<List<Customer>> GetCustomersByAgentIdAsync(int agentId);
        public Task AddCustomerWithUser(Customer customer, User user);
        public Task<Customer> GetByUserId(int Id, string[] innerTables);
        public Task<PageList<Customer>> GetAsync(Parameter pageParameter);
        public  Task<Customer> GetCustomerByUserIDAsync(int Id);
        public  Task<Customer> GetByCustomerId(int Id, string[] innerTables);
        public  Task<PageList<Policy>> GetCustomerWithPolicy(int userID, Parameter filterParameter);
        public Customer GetByPhone(string username);


        public Customer GetByEmail(string username);
       


    }  
}
