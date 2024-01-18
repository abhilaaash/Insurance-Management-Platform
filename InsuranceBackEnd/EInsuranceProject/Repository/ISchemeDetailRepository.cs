using EInsuranceProject.Model;

namespace EInsuranceProject.Repository
{
    public interface ISchemeDetailRepository:IEntityRepository<SchemeDetails>
    {
        public Task<SchemeDetails> GetSchemeDetailsByIdAsync(int detailId);
    }
}
