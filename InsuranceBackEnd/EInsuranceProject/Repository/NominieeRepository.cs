using EInsuranceProject.Data;
using EInsuranceProject.Model;
using Microsoft.EntityFrameworkCore;

namespace EInsuranceProject.Repository
{
    public class NominieeRepository: EntityRepository<Nominie>, INominieeRepository
    {
        private InsuranceContext _context;

        public NominieeRepository(InsuranceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Nominie>> GetNominieeByCustomerIdAsync(int customerId)
        {
            return await _context.Nominies
                .Where(c => c.NominieId == customerId)
                .ToListAsync();
        }
    }
}
