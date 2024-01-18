using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using EInsuranceProject.Repository;
using System.Linq.Expressions;

namespace EInsuranceProject.Services
{
    public class SchemeDetailsService:ISchemeDetailsService
    {
        private IEntityRepository<SchemeDetails> _repository;
        private ISchemeDetailRepository _schemeDetailRepository;

        public SchemeDetailsService(IEntityRepository<SchemeDetails> entityRepository, ISchemeDetailRepository schemeDetailRepository)
        {
            _repository = entityRepository;
            _schemeDetailRepository = schemeDetailRepository;
        }
        public async Task AddSchemeDetail(SchemeDetails details)
        {
            await _repository.Insert(details);
        }

        public async Task<bool> DeleteSchemeDetail(int id)
        {
            var DetailToDelete = await _repository.GetById(id);
            if (DetailToDelete != null)
                return await _repository.Delete(id);
            throw new DataNotFoundExcpetion("No Scheme Detail Data found");
        }

        public async Task<IEnumerable<SchemeDetails>> GetAll(string[] innerTables)
        {
            Expression<Func<SchemeDetails, bool>> filterPredicate = e => e.Status == true;
            var schemeDetails = await _repository.GetAll(innerTables,filterPredicate);
            if (schemeDetails.Any())
            {
                return schemeDetails;
            }
            throw new DataNotFoundExcpetion("No Scheme Detail Data found");
        }

        public async Task<SchemeDetails> GetById(int Id)
        {
            var details = await _repository.GetById(Id);
            if (details != null)
            {
                return details;
            }
            throw new DataNotFoundExcpetion("No detail Data found");
        }

        public async Task<bool> UpdateSchemeDetail(SchemeDetails details)
        {
            var DetailToUpdate = await _repository.GetById(details.DetailId);
            if (DetailToUpdate != null)
            {
                return await _repository.Update(details, details.DetailId);
            }
            throw new DataNotFoundExcpetion("No Detail Data found");
        }
        public async Task<SchemeDetails> GetSchemeDetailsByIdAsync(int detailId)
        {
            return await _schemeDetailRepository.GetSchemeDetailsByIdAsync(detailId);
        }
    }
}
