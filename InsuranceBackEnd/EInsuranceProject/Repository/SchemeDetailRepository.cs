using EInsuranceProject.Data;
using EInsuranceProject.Model;
using Microsoft.EntityFrameworkCore;

namespace EInsuranceProject.Repository
{
    public class SchemeDetailRepository:EntityRepository<SchemeDetails>,ISchemeDetailRepository
    {
        private InsuranceContext _context;
        public SchemeDetailRepository(InsuranceContext context):base(context)
        {
            _context = context;
        }
        public async Task<SchemeDetails> GetSchemeDetailsByIdAsync(int detailId)
        {
            return await _context.SchemeDetails.FirstOrDefaultAsync(sd => sd.DetailId == detailId);
        }
    }
}
