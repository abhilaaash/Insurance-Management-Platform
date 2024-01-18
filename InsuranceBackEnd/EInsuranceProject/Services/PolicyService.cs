using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using EInsuranceProject.Repository;
using System.Linq.Expressions;

namespace EInsuranceProject.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _policyRepository;

        private IEntityRepository<Policy> _repository;
        public PolicyService(IEntityRepository<Policy>repository, IPolicyRepository policyRepository) 
        {
            _repository = repository;
            _policyRepository = policyRepository;
        }

        public async Task AddPolicy(Policy policy)
        {
            await _repository.Insert(policy);
        }

        public async Task<bool> DeletePolicy(int id)
        {
            var PolicyToDelete = await _repository.GetById(id);
            if (PolicyToDelete != null)
                return await _repository.Delete(id);
            throw new DataNotFoundExcpetion("No Policy Data found");
        }

        public async Task<IEnumerable<Policy>> GetAll(string[] innerTables)
        {
            Expression<Func<Policy, bool>> filterPredicate = e => e.Status == true;
            var policies = await _repository.GetAll(innerTables, filterPredicate);
            if (policies.Any())
            {
                return policies;
            }
            throw new DataNotFoundExcpetion("No policy Data found");
        }

        public async Task<Policy> GetById(int Id)
        {
            Expression<Func<Policy, bool>> filterPredicate = e => e.PolicyId == Id;
            string[] innerTable = { "Payments" };
            var policy = await _repository.GetById(filterPredicate,innerTable);
            if (policy != null)
            {
                return policy;
            }
            throw new DataNotFoundExcpetion("No policy Data found");
        }

        public async Task<bool> UpdatePolicy(Policy policy)
        {
            var policyToUpdate = await _repository.GetById(policy.PolicyId);
            if (policyToUpdate != null)
            {
                return await _repository.Update(policy, policy.PolicyId);
            }
            throw new DataNotFoundExcpetion("No Policy Data found");
        }




        //--------------------------------------------------------------------------------------------
        public async Task<List<Policy>> GetPoliciesByCustomerId(int customerId)
        {
            return await _policyRepository.GetPoliciesByCustomerIdAsync(customerId);
        }

        public async Task<List<Policy>> GetPoliciesByAgentIdAsync(int agentId)
        {
            return await _policyRepository.GetPoliciesByAgentIdAsync(agentId);
        }

        public async Task<PageList<Policy>> GetAsync(PageParameters pageParameter)
        {
            Expression<Func<Policy, bool>> filterPredicate = e => e.Status == true;
            var customers = await _repository.GetAsync(filterPredicate);
            return PageList<Policy>.ToPagedList(customers, pageParameter.PageNumber, pageParameter.PageSize);
        }


        public async Task<Policy> AddPolicyReturn(Policy policy)
        {
            return await _repository.InsertTest(policy);
        }

    }
}
