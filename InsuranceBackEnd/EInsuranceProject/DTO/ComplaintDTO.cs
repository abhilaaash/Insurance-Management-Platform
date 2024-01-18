using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class ComplaintDTO
    {
  //      public int ComplaintId { get; set; }

       [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^[a-zA-Z][a-zA-Z\\s]*$", ErrorMessage = "Invalid format for Complaint Name.")]
        public string ComplaintName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? ComplaintMessage { get; set; }

        [CustomNonNegativeNumber(ErrorMessage = "CustomerId must be non-negative.")]
        public int CustomerId { get; set; }

    }
}
