using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class ComplaintUpdateDTO
    {
        public int ComplaintId { get; set; }
        public string? Reply { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string ComplaintName { get; set; }
        public string? ComplaintMessage { get; set; }

      
        public DateTime DateOfComplaint { get; set; }
        public int CustomerId { get; set; }
    }
}
