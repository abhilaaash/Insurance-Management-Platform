using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class ComplaintShowDTO
    {
        public int ComplaintId { get; set; }
        public string ComplaintName { get; set; }
        public string? ComplaintMessage { get; set; }
        public DateTime DateOfComplaint { get; set; }
        public string? Reply { get; set; }
        public int CustomerId { get; set; }
    }
}
