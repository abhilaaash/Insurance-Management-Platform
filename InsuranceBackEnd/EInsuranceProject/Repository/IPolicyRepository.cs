using EInsuranceProject.Model;

namespace EInsuranceProject.Repository
{
    public interface IPolicyRepository
    {
        public Task<List<Policy>> GetPoliciesByCustomerIdAsync(int customerId);
        public Task<List<Policy>> GetPoliciesByAgentIdAsync(int agentId);
    }
}
