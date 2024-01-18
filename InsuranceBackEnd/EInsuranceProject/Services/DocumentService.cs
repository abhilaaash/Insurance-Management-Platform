using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using EInsuranceProject.Repository;
using EInsuranceProject.Wrapper;
using System.Linq.Expressions;

namespace EInsuranceProject.Services
{
    public class DocumentService : IDocumentService
    {
        

        private IDocumentRepository _documentRepository;
        private IEntityRepository<Document> _repository;

        public DocumentService(IEntityRepository<Document> entityRepository, IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
            _repository = entityRepository;
        }
        public async Task AddDocument(Document document)
        {
            await _repository.Insert(document);
        }

        public async Task<bool> DeleteDocument(int id)
        {
            var DocToDelete = await _repository.GetById(id);
            if (DocToDelete != null)
                return await _repository.Delete(id);
            throw new DataNotFoundExcpetion("No Document Data found");
        }

        public async Task<IEnumerable<Document>> GetAll(string[] innerTables)
        {
            Expression<Func<Document, bool>> filterPredicate = e => e.DocumentId!=null;
            var documents = await _repository.GetAll(innerTables, filterPredicate);
            if (documents.Any())
            {
                return documents;
            }
            throw new DataNotFoundExcpetion("No Document  found");
        }

        public async Task<Document> GetById(int Id)
        {
            var document = await _repository.GetById(Id);
            if (document != null)
            {
                return document;
            }
            throw new DataNotFoundExcpetion("No document Data found");
        }

        public async Task<bool> UpdateDocument(Document document)
        {
            var DocToUpdate = await _repository.GetById(document.DocumentId);
            if (DocToUpdate != null)
            {
                return await _repository.Update(document, document.DocumentId);
            }
            throw new DataNotFoundExcpetion("No Document Data found");
        }
        public async Task<List<Document>> GetDocumentsByCustomerIdAsync(int customerId)
        {
            return await _documentRepository.GetDocumentsByCustomerIdAsync(customerId);
        }

        //public async Task<PageList<Document>> GetAsync(PageParameters pageParameter)
        //{
        //    Expression<Func<Document, bool>> filterPredicate = e => e.DocumentId != null;

        //    var customers = await _repository.GetAsync(filterPredicate);
        //    return PageList<Document>.ToPagedList(customers, pageParameter.PageNumber, pageParameter.PageSize);
        //}
        public async Task<PageList<Document>> GetAsync(Parameter parameter)
        {
            Expression<Func<Document, bool>> filterPredicate = e => e.DocumentId != null;
            if (parameter.Id != null)
            {
                filterPredicate = e =>  e.CustomerId == parameter.Id;
            }
            else if (parameter.FirstName != null)
            {
                filterPredicate = e => e.Customer.CustomerFirstName.Contains(parameter.FirstName);
            }
            

            else
            {
                filterPredicate = e => e.DocumentId != null;
            }

            var customers = await _repository.GetAsync(filterPredicate);
            return PageList<Document>.ToPagedList(customers, parameter.PageNumber, parameter.PageSize);
        }
        public async Task<string> UploadDocument(IFormFile file, Document document)
        {
            var fileName = await WriteFile(file);
            document.DocumentName = fileName;
            document.FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", fileName);
            await _documentRepository.AddDocument(document);
            return fileName;

        }
        public async Task<string> WriteFile(IFormFile file)
        {
            string filename = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                filename = DateTime.Now.Ticks.ToString() + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return filename;
        }

        public async Task<bool> UpdateDocumentStatus(int documentId)
        {
            try
            {
                await _documentRepository.Update(documentId);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
        public async Task<PageList<Document>> GetByCustomerIdAsync(PageParameters pageParameter, int customerID)
        {
            var documents = await _documentRepository.GetByCustomerId(customerID);
            if (documents.Any())
            {
                return PageList<Document>.ToPagedList(documents, pageParameter.PageNumber, pageParameter.PageSize);

            }
            throw new DataNotFoundExcpetion("No data found");
        }

       
    }
}
