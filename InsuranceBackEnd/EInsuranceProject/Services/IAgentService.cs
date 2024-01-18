using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.Model;
using EInsuranceProject.Wrapper;

namespace EInsuranceProject.Services
{
    public interface IAgentService
    {
        public Task<IEnumerable<Agent>> GetAll(string[] innerTables);
        public Task<Agent> GetById(int Id, string[] innerTables);
        public Task AddAgent(Agent agent);
        public Task<bool> UpdateAgent(Agent agent);
        public Task<bool> DeleteAgent(int id);
        public  Task AddAgentWithUser(Agent admin, User user);

        public Task<PageList<Agent>> GetAsync(Parameter pageParameter);
        public  Task<Agent> GetByAgentId(int Id, string[] innerTables);
    }
}
