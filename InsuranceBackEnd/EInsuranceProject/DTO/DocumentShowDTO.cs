using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class DocumentShowDTO
    {
        public int DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public int? PolicyNo { get; set; }
        public int CustomerId { get; set; }
    }
}
