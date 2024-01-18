using EInsuranceProject.Data;
using EInsuranceProject.Model;
using Microsoft.EntityFrameworkCore;

namespace EInsuranceProject.Repository
{
    public class PolicyRepository: EntityRepository<Policy>,IPolicyRepository
    {
        private InsuranceContext _context;
        public PolicyRepository(InsuranceContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Policy>> GetPoliciesByCustomerIdAsync(int customerId)
        {
            return await _context.Policies
                .Where(policy => policy.CustomerId == customerId)
                .ToListAsync();
        }
        public async Task<List<Policy>> GetPoliciesByAgentIdAsync(int agentId)
        {
            return await _context.Policies
                .Where(p => p.AgentId == agentId)
                .ToListAsync();
        }
    }
}
