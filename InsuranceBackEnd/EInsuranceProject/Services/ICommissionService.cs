using FinalProjectInsurance.Model;

namespace FinalProjectInsurance.Service
{
    public interface ICommissionService
    {
        public Task<IEnumerable<Commission>> GetAll(string[] innerTables);
        public Task<Commission> GetById(int Id);
        public Task AddCommission(Commission Commission);
        public Task<bool> UpdateCommission(Commission Commission);
        public Task<bool> DeleteCommission(int id);
        public Task<List<Commission>> GetCommissionByPolicyIdAsync(int policyId);
        public Task<List<Commission>> GetCommissionsByAgentIdAsync(int agentId);
    }
}
