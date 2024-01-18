using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsuranceProject.Model
{
    public class Complaint
    {
        [Key]
        public int ComplaintId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^[a-zA-Z][a-zA-Z\\s]*$", ErrorMessage = "Invalid format for Complaint Name.")]
        public string ComplaintName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? ComplaintMessage { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [CustomDateOfComplaint(ErrorMessage = "Invalid format for Date of Complaint.")]
        public DateTime DateOfComplaint { get; set; }


        public string? Reply { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public bool Status { get; set; }

        public Customer? Customer { get; set; }

        [ForeignKey("Customer")]
        [Required(ErrorMessage = "CustomerId is required")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]
        public int CustomerId { get; set; }
    }
}
