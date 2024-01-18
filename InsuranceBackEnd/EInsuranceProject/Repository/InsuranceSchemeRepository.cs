using EInsuranceProject.Data;
using EInsuranceProject.Model;
using Microsoft.EntityFrameworkCore;

namespace EInsuranceProject.Repository
{
    public class InsuranceSchemeRepository : EntityRepository<InsuranceScheme>, IInsuranceSchemeRepository
    {
        private InsuranceContext _context;

        public InsuranceSchemeRepository(InsuranceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<InsuranceScheme>> GetInsuranceSchemeByPlanIdAsync(int planId)
        {
            return await _context.InsuranceSchemes
                .Where(c => c.InsurancePlanId == planId)
                .ToListAsync();
        }
    }
}
