using EInsuranceProject.Data;
using EInsuranceProject.Model;
using Microsoft.EntityFrameworkCore;

namespace EInsuranceProject.Repository
{
    public class DocumentRepository : EntityRepository<Document>, IDocumentRepository
    {

        private InsuranceContext _context;

        public DocumentRepository(InsuranceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Document>> GetDocumentsByCustomerIdAsync(int customerId)
        {
            return await _context.Documents
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();
        }
        public async Task AddDocument(Document document)
        {
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Document>> GetByCustomerId(int customerId)
        {
            return await _context.Documents.Where(d => d.CustomerId == customerId).ToListAsync();
        }
        public async Task Update(int documentId)
        {

            var document = _context.Documents.Where(d => d.DocumentId == documentId).FirstOrDefault();
            document.status = true;
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
        }

    }
}
