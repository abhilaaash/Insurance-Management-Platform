using EInsuranceProject.Data;
using EInsuranceProject.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Claim = EInsuranceProject.Model.Claim;

namespace EInsuranceProject.Repository
{
    public class ClaimRepository: EntityRepository<Claim>,IClaimRepository
    {
        private InsuranceContext _context;
        public  ClaimRepository(InsuranceContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Claim>> GetClaimByCustomerIdAsync(int customerId)
        {
            return await _context.Claims
                .Where(policy => policy.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
