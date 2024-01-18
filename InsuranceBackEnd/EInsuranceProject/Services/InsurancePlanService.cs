using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using EInsuranceProject.Repository;
using System.Linq.Expressions;

namespace EInsuranceProject.Services
{
    public class InsurancePlanService : IInsurancePlanService
    {
        private IEntityRepository<InsurancePlan> _repository;

        public InsurancePlanService(IEntityRepository<InsurancePlan> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<InsurancePlan>> GetAll(string[] innerTables)
        {
            Expression<Func<InsurancePlan, bool>> filterPredicate = e => e.Status != null;
            var InsurancePlan=await _repository.GetAll(innerTables, filterPredicate);
            if(InsurancePlan.Any()) 
            {
                return InsurancePlan;
            }
            throw new DataNotFoundExcpetion("No Plans available");
        }
        public async Task<InsurancePlan> GetById(int Id, string[] innerTable)
        {
            Expression<Func<InsurancePlan, bool>> filterPredicate = e => e.Status != null && e.PlanId==Id;
            var InsurancePlan=await _repository.GetById(filterPredicate,innerTable);
            if (InsurancePlan != null)
                return InsurancePlan;
            throw new DataNotFoundExcpetion("No Such Plan Found");
        }
        public async Task<InsurancePlan> CheckPlaneName(string plan, string[] innerTable)
        {
            Expression<Func<InsurancePlan, bool>> filterPredicate = e => e.Status != null && e.PlanName == plan;
            var InsurancePlan = await _repository.GetById(filterPredicate, innerTable);
            if (InsurancePlan != null)
                return InsurancePlan;
            else
                return null;
           
        }
        public async Task AddInsurancePlan(InsurancePlan plan)
        {
            await _repository.Insert(plan);
        }

        public async Task<bool> DeletePlan(int id)
        {
            var PlanToDelete = await _repository.GetById(id);
            if(PlanToDelete != null)
                return await _repository.Delete(id);
            throw new DataNotFoundExcpetion("No such plan found to delete");
            
        }

       

      

        public async Task<bool> UpdatePlan(InsurancePlan plan)
        {
            var PlanToDelete = await _repository.GetById(plan.PlanId);
            if(PlanToDelete!=null)
                return await _repository.Update(plan,plan.PlanId);
            throw new DataNotFoundExcpetion("No such plan found to update");
        }
    }
}
