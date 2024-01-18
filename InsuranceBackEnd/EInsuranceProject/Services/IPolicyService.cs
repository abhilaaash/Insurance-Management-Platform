using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.Model;

namespace EInsuranceProject.Services
{
    public interface IPolicyService
    {
        public Task<IEnumerable<Policy>> GetAll(string[] innerTables);
        public Task<Policy> GetById(int Id);
        public Task AddPolicy(Policy policy);
        public Task<bool> UpdatePolicy(Policy policy);
        public Task<bool> DeletePolicy(int id);
        public Task<List<Policy>> GetPoliciesByCustomerId(int customerId);
        public Task<List<Policy>> GetPoliciesByAgentIdAsync(int agentId);
        public Task<Policy> AddPolicyReturn(Policy policy);
        public Task<PageList<Policy>> GetAsync(PageParameters pageParameter);


    }
}
