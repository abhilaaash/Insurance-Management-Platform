using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsuranceProject.Model
{
    public class Claim
    {
        [Key]
        
        public int ClaimId { get; set; }

        public double ClaimAmount { get; set; }
        [Required (ErrorMessage ="This field is required")]
        public DateTime ClaimDate { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? BankAccountNo { get;set; }
        [Required(ErrorMessage = "This field is required")]

        public string? BankIFSCCode { get;set; }
        public bool Status { get; set; }

        public Policy ?Policy { get; set; }
        [ForeignKey("Policy")]
        public int PolicyNo { get; set; }

        public Customer Customer { get; set; }
        [ForeignKey("Customer")]

        public int CustomerId { get; set; }
    }
}
