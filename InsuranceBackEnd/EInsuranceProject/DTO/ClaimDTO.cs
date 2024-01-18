using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class ClaimDTO
    {
        public int ClaimId { get; set; }
       
        public double ClaimAmount { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public DateTime ClaimDate { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? BankAccountNo { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? BankIFSCCode { get; set; }
       // public bool Status { get; set; }
        public int PolicyNo { get; set; }
        public int CustomerId { get; set; }

    }
}
