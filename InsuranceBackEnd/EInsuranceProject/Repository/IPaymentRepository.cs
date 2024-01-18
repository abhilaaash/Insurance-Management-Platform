using EInsuranceProject.Model;

namespace EInsuranceProject.Repository
{
    public interface IPaymentRepository:IEntityRepository<Payment>
    {
        public Task<List<Payment>> GetPaymentsByCustomerIdAsync(int customerId);
    }
}
