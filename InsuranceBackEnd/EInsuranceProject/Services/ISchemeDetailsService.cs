using EInsuranceProject.Model;

namespace EInsuranceProject.Services
{
    public interface ISchemeDetailsService
    {
        public Task<IEnumerable<SchemeDetails>> GetAll(string[] innerTables);
        public Task<SchemeDetails> GetById(int Id);
        public Task AddSchemeDetail(SchemeDetails details);
        public Task<bool> UpdateSchemeDetail(SchemeDetails schemeDetails);
        public Task<bool> DeleteSchemeDetail(int id);
        public Task<SchemeDetails> GetSchemeDetailsByIdAsync(int detailId);
    }
}
