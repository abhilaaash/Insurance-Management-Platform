using EInsuranceProject.Data;
using EInsuranceProject.Model;

namespace EInsuranceProject.Repository
{
    public class PlanSchemeDetailsRepository:EntityRepository<InsuranceScheme>, IPlanSchemeDetailsRepository
    {
        private InsuranceContext _context;
        public PlanSchemeDetailsRepository(InsuranceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> AddScheme(InsuranceScheme scheme)
        {
         //   scheme.SchemeDetails = schemeDetails;
            _context.InsuranceSchemes.Add(scheme);
            await _context.SaveChangesAsync();
            return scheme.SchemeId;
        }
        public async Task AddSchemeDetails(SchemeDetails schemeDetails)
        {
            _context.SchemeDetails.Add(schemeDetails);
            await _context.SaveChangesAsync();
        }
    }
}
