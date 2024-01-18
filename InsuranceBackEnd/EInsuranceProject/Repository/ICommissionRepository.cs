using FinalProjectInsurance.Model;

namespace EInsuranceProject.Repository
{
    public interface ICommissionRepository:IEntityRepository<Commission>
    {
        public Task<List<Commission>> GetByPolicyIdAsync(int policyId);
        public Task<List<Commission>> GetByAgentIdAsync(int agentId);
    }
}
