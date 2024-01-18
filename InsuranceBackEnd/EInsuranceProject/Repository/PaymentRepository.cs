using EInsuranceProject.Data;
using EInsuranceProject.Model;
using Microsoft.EntityFrameworkCore;

namespace EInsuranceProject.Repository
{
    public class PaymentRepository:EntityRepository<Payment>,IPaymentRepository
    {
        private readonly InsuranceContext _context;

        public PaymentRepository(InsuranceContext context):base(context) 
        {
            _context = context;
        }

        public async Task<List<Payment>> GetPaymentsByCustomerIdAsync(int customerId)
        {
            return await _context.Payments
                .Where(p => p.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
