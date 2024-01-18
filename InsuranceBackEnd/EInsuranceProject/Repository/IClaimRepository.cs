using EInsuranceProject.Model;

namespace EInsuranceProject.Repository
{
    public interface IClaimRepository : IEntityRepository<Claim>
    {
        public  Task<List<Claim>> GetClaimByCustomerIdAsync(int customerId);

    }
}
