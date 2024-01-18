using EInsuranceProject.Model;
using EInsuranceProject.Repository;
using FinalProjectInsurance.Model;
using System.Linq.Expressions;

namespace FinalProjectInsurance.Service
{
    public class CommissionService:ICommissionService
    {
        private ICommissionRepository _repository;
        private IEntityRepository<Commission> _entityRepository;

        public CommissionService(IEntityRepository<Commission> entityRepository, ICommissionRepository repository)
        {
            _entityRepository = entityRepository;
            _repository = repository;
        }
        public async Task AddCommission(Commission commission)
        {
            await _entityRepository.Insert(commission);
        }

        public async Task<bool> DeleteCommission(int id)
        {
            var commissionToDelete = await _entityRepository.GetById(id);
            if (commissionToDelete != null)
                return await _entityRepository.Delete(id);
            throw new Exception("No Commission Data Found To Delete");
        }

        public async Task<IEnumerable<Commission>> GetAll(string[] innerTables)
        {
            Expression<Func<Commission, bool>> filterPredicate = e => e.status!=null;
            var commission = await _entityRepository.GetAll(innerTables, filterPredicate);
            if (commission.Count() > 0)
            {
                return commission;
            }
            throw new Exception("No Commission Data found");

        }

        public async Task<Commission> GetById(int Id)
        {
            var commission = await _entityRepository.GetById(Id);
            if (commission != null)
                return commission;
            throw new Exception("No Commission Data Found");
        }

        public async Task<bool> UpdateCommission(Commission commission)
        {
            var commissionToUpdate = await _entityRepository.GetById(commission.CommissionId);
            if (commissionToUpdate != null)
                return await _entityRepository.Update(commission, commission.CommissionId);
            throw new Exception("No Commission Data Found To Update");
        }
        public async Task<List<Commission>> GetCommissionByPolicyIdAsync(int policyId)
        {
            var commission= await _repository.GetByPolicyIdAsync(policyId);
            if (commission != null)
                return commission;
            throw new Exception("No Commision data found for Policy");
        }

        public async Task<List<Commission>> GetCommissionsByAgentIdAsync(int agentId)
        {
       
            var commission = await _repository.GetByAgentIdAsync(agentId);
            if (commission != null)
                return commission;
            throw new Exception("No Commision data found for Agent");
        }


    }
}
