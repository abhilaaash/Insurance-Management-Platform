using FinalProjectInsurance.Model;

namespace FinalProjectInsurance.Service
{
    public interface IResponseService
    {
        public Task<IEnumerable<Response>> GetAll(string[] innerTables);
        public Task<Response> GetById(int Id);
        public Task AddResponse(Response Response);
        public Task<bool> UpdateResponse(Response Response);
        public Task<bool> DeleteResponse(int id);
    }
}
