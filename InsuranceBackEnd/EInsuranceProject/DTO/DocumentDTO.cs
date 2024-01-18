using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsuranceProject.DTO
{
    public class DocumentDTO
    {
        public int DocumentId { get; set; }
        [Required(ErrorMessage = "This field is required")]

        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        // public string DoumentFile { get;set; }

        public int CustomerId { get; set; }
        public bool Status { get; set; }
    }
}
