using EInsuranceProject.Model;

namespace EInsuranceProject.Repository
{
    public interface IInsuranceSchemeRepository:IEntityRepository<InsuranceScheme>
    {
        public Task<List<InsuranceScheme>> GetInsuranceSchemeByPlanIdAsync(int planId);
    }
}
