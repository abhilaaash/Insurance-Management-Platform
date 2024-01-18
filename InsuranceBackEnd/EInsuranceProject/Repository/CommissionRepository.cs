using EInsuranceProject.Data;
using FinalProjectInsurance.Model;
using Microsoft.EntityFrameworkCore;

namespace EInsuranceProject.Repository
{
    public class CommissionRepository:EntityRepository<Commission>, ICommissionRepository
    {
        private readonly InsuranceContext _context;

        public CommissionRepository(InsuranceContext context):base(context)
        {
            _context = context;
        }

        public async Task<List<Commission>> GetByPolicyIdAsync(int policyId)
        {
            return await _context.Commissions
                .Where(c => c.PolicyId == policyId).ToListAsync();
        }

        public async Task<List<Commission>> GetByAgentIdAsync(int agentId)
        {
            return await _context.Commissions
                .Where(c => c.AgentId == agentId)
                .ToListAsync();
        }
    }
}
