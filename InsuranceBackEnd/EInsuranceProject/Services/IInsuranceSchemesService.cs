using EInsuranceProject.Model;

namespace EInsuranceProject.Services
{
    public interface IInsuranceSchemesService
    {
        public Task<IEnumerable<InsuranceScheme>> GetAll(string[] innerTables);
        public Task<InsuranceScheme> GetById(int Id);
        public Task AddScheme(InsuranceScheme scheme);
        public Task<bool> UpdateScheme(InsuranceScheme scheme);
        public Task<bool> DeleteScheme(int id);
        public  Task<List<InsuranceScheme>> GetInsuranceSchemeByPlanIdAsync(int planId);
        public Task AddSchemeWithDetails(InsuranceScheme scheme, SchemeDetails schemeDetails);
    }
}
