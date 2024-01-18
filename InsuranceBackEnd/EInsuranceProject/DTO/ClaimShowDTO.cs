using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class ClaimShowDTO
    {
        public int ClaimId { get; set; }

        public double ClaimAmount { get; set; }
        public DateTime ClaimDate { get; set; }
        public string? BankAccountNo { get; set; }
        public string? BankIFSCCode { get; set; }
        public bool status { get; set; }
        public int PolicyNo { get; set; }
        public int CustomerId { get; set; }
    }
}
