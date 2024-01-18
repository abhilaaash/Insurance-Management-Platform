using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using EInsuranceProject.Repository;
using System.Linq.Expressions;

namespace EInsuranceProject.Services
{
    public class NominieService:INominieService
    {
        private INominieeRepository _nominieeRepository;
        private IEntityRepository<Nominie> _repository;
        public NominieService(IEntityRepository<Nominie> repository, INominieeRepository nominieeRepository)
        {
            _repository = repository;
            _nominieeRepository = nominieeRepository;
        }
        public async Task AddNominie(Nominie nominie)
        {
            await _repository.Insert(nominie);
        }

        public async Task<bool> DeleteNominie(int id)
        {
            var nominieToDelete = await _repository.GetById(id);
            if (nominieToDelete != null)
                return await _repository.Delete(id);
            throw new DataNotFoundExcpetion("No Nominie Data found");
        }

        public async Task<IEnumerable<Nominie>> GetAll(string[] innerTables)
        {
            Expression<Func<Nominie, bool>> filterPredicate = e => e.Status == true;
            var nominie = await _repository.GetAll(innerTables, filterPredicate);
            if (nominie.Count() > 0)
            {
                return nominie;
            }
            throw new DataNotFoundExcpetion("No Nominie Data found");
        }

        public async Task<Nominie> GetById(int Id)
        {
            Expression<Func<Nominie, bool>> filterPredicate = e => e.Status == true;
            var nominie = await _repository.GetById(Id);
            if (nominie != null)
            {
                return nominie;
            }
            throw new DataNotFoundExcpetion("No Nominie Data found");
        }

        public async Task<bool> UpdateNominie(Nominie nominie)
        {
            var nominieToUpdate = await _repository.GetById(nominie.NominieId);
            if (nominieToUpdate != null)
            {
                return await _repository.Update(nominie, nominie.NominieId);
            }
            throw new DataNotFoundExcpetion("No Nominie Data found");
        }
        public async Task<List<Nominie>> GetNominieeByCustomerIdAsync(int customerId)
        {

            return await _nominieeRepository.GetNominieeByCustomerIdAsync(customerId);
        }

    }
}
