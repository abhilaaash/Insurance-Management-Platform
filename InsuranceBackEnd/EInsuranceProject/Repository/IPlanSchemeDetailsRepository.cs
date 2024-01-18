using EInsuranceProject.Model;
using Microsoft.EntityFrameworkCore;

namespace EInsuranceProject.Repository
{
    public interface IPlanSchemeDetailsRepository:IEntityRepository<InsuranceScheme>
    {
        public  Task<int> AddScheme(InsuranceScheme scheme);

        public  Task AddSchemeDetails(SchemeDetails schemeDetails);
    }
}
