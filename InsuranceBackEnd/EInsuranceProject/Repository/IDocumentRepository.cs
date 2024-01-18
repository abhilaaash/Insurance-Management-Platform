using EInsuranceProject.Model;
using Microsoft.EntityFrameworkCore;

namespace EInsuranceProject.Repository
{
    public interface IDocumentRepository : IEntityRepository<Document>
    {
        public  Task<List<Document>> GetDocumentsByCustomerIdAsync(int customerId);


        public  Task AddDocument(Document document);

        public Task<List<Document>> GetByCustomerId(int customerId);

        public Task Update(int documentId);
       
    }
}
