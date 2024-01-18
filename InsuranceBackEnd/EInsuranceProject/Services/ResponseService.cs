using EInsuranceProject.Model;
using EInsuranceProject.Repository;
using FinalProjectInsurance.Model;
using System.Linq.Expressions;

namespace FinalProjectInsurance.Service
{
    public class ResponseService : IResponseService
    {
        private IEntityRepository<Response> _entityRepository;
        public ResponseService(IEntityRepository<Response> entityRepository)
        {
            _entityRepository = entityRepository;
        }
        public async Task AddResponse(Response response)
        {
            await _entityRepository.Insert(response);
        }

        public async Task<bool> DeleteResponse(int id)
        {
            var ResponseToDelete = await _entityRepository.GetById(id);
            if (ResponseToDelete != null)
                return await _entityRepository.Delete(id);
            throw new Exception("No Feedback Found To Delete");
        }

        public async Task<IEnumerable<Response>> GetAll(string[] innerTables)
        {
            Expression<Func<Response, bool>> filterPredicate = e => e.Status == true;
            var responses = await _entityRepository.GetAll(innerTables, filterPredicate);
            if (responses.Count() > 0)
            {
                return responses;
            }
            throw new Exception("No response found");

        }
      
        
        public async Task<Response> GetById(int Id)
        {
            var response = await _entityRepository.GetById(Id);
            if (response != null)
                return response;
            throw new Exception("No Response Found");
        }

        public async Task<bool> UpdateResponse(Response response)
        {
            var ResponseToUpdate = await _entityRepository.GetById(response.ResponseId);
            if (ResponseToUpdate != null)
                return await _entityRepository.Update(response, response.ResponseId);
            throw new Exception("No Response Found To Update");
        }
    }
}

