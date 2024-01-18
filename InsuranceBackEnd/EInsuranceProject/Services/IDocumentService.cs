using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using System.Linq.Expressions;

namespace EInsuranceProject.Services
{
    public interface IDocumentService
    {
        public Task<bool> DeleteDocument(int id);

        public Task<IEnumerable<Document>> GetAll(string[] innerTables);


        public Task<Document> GetById(int Id);



        public Task<bool> UpdateDocument(Document document);

        public  Task<bool> UpdateDocumentStatus(int documentId);

        public Task<List<Document>> GetDocumentsByCustomerIdAsync(int customerId);


        public  Task<PageList<Document>> GetAsync(Parameter parameter);

        public Task<PageList<Document>> GetByCustomerIdAsync(PageParameters pageParameter, int customerID);

        public Task<string> UploadDocument(IFormFile file, Document document);


        public Task<string> WriteFile(IFormFile file);
       
    }
}
