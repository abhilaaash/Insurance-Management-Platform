using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class ClaimAddDTO
    {
        [Required(ErrorMessage = "This field is required")]
        public double ClaimAmount { get; set; }
        [Required(ErrorMessage = "This field is required")]
     //   public DateTime ClaimDate { get; set; }
     //   
        public string? BankAccountNo { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? BankIFSCCode { get; set; }
        // public bool Status { get; set; }
        public int PolicyNo { get; set; }
        public int CustomerId { get; set; }
    }
}
