using EInsuranceProject.Model;

namespace EInsuranceProject.Repository
{
    public interface INominieeRepository : IEntityRepository<Nominie>
    {
        public Task<List<Nominie>> GetNominieeByCustomerIdAsync(int customerId);
    }
}
