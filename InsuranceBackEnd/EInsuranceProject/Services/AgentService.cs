using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.DTO;
using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using EInsuranceProject.Repository;
using EInsuranceProject.Wrapper;
using System.Linq.Expressions;

namespace EInsuranceProject.Services
{
    public class AgentService : IAgentService
    {
        private IAgentRepository _repository;

        public AgentService(IAgentRepository Repository)
        {
            _repository = Repository;
        }
        public async Task AddAgent(Agent agent)   {
           await  _repository.Insert(agent);
        }

        public async Task<bool> DeleteAgent(int id)
        {
            var AgentToDelete = await _repository.GetById(id);
            if (AgentToDelete != null)
                return await _repository.Delete(id);
            throw new DataNotFoundExcpetion("No Agent Data found");
        }

        public async Task<IEnumerable<Agent>> GetAll(string[] innerTables)
        {
            
            Expression<Func<Agent, bool>> filterPredicate = e => e.Status !=null;
            var agents=await _repository.GetAll(innerTables, filterPredicate);
            if(agents.Any())
            {
                return agents;
            }
            throw new DataNotFoundExcpetion("No agent Data found");
        }

        public async Task<Agent> GetById(int Id,string[] innerTables )

        { 
            Expression<Func<Agent, bool>> filterPredicate = e => e.Status !=null  && e.UserId==Id;
            var agent=await _repository.GetById(filterPredicate,innerTables);
            if(agent!=null)
            {
                return agent;
            }
            throw new DataNotFoundExcpetion("No agent Data found");
        }

        public async Task<Agent> GetByAgentId(int Id, string[] innerTables)

        {
            Expression<Func<Agent, bool>> filterPredicate = e => e.Status != null && e.AgentId == Id;
            var agent = await _repository.GetById(filterPredicate, innerTables);
            if (agent != null)
            {
                return agent;
            }
            throw new DataNotFoundExcpetion("No agent Data found");
        }

        public async Task<bool> UpdateAgent(Agent agent)
        {
            var agentToUpdate = await _repository.GetById(agent.AgentId);
            if(agentToUpdate!=null)
            {
                return await _repository.Update(agent, agent.AgentId);
            }
            throw new DataNotFoundExcpetion("No Agent Data found");
        }
        public async Task AddAgentWithUser(Agent admin, User user)
        {
            int id = await _repository.AddUser(user);
            admin.UserId = id;
            await _repository.AddAgentWithUser(admin);
        }



        public async Task<PageList<Agent>> GetAsync(Parameter parameter)
        {
            Expression<Func<Agent, bool>> filterPredicate = e => e.Status == true;
            if (parameter.Id != null)
            {
                filterPredicate = e => e.Status != null && e.AgentId == parameter.Id;
            }
            else if (parameter.FirstName != null)
            {
                filterPredicate = e => e.Status != null && e.AgentFirstName.Contains(parameter.FirstName);
            }
            else
            {
                filterPredicate = e => e.Status != null;
            }
            var customers = await _repository.GetAsync(filterPredicate);
            return PageList<Agent>.ToPagedList(customers, parameter.PageNumber, parameter.PageSize);
        }
    }
}
