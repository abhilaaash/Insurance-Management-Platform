using EInsuranceProject.DTO;
using EInsuranceProject.Model;

namespace EInsuranceProject.Repository
{
    public interface IAgentRepository:IEntityRepository<Agent>
    {
        public Task<int> AddUser(User user);

        public Task AddAgentWithUser(Agent admin);
    }
}
