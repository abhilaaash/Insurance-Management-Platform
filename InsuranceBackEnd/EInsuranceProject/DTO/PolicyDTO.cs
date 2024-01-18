using EInsuranceProject.Model;
using FinalProjectInsurance.DTO;
using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class PolicyDTO
    {
    //    public int PolicyNo { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }
        [Required]
        public DateTime MaturityDate { get; set; }
        //    [Required]
        //    public Mode PremiumMode { get; set; }
        public Mode PremiumMode { get; set; }

        public int TotalPremiumNo { get; set; }
        [Required]
        public double Premium { get; set; }
        [Required]
        public double SumAssured { get; set; }
        public  int SchemeId { get; set; }
     //   public bool Status { get; set; }
        public int CustomerId { get; set; }
        public int? AgentId { get; set; } = 0;
        //  public int NomineeCount { get; set; }
        public PaymentDTO PaymentDTO { get; set; }
        public NomineeDTO? NomineeDTO { get; set; }



        // chnged justtttt 
       
        //     public int CustomersCount { get; set; }
        //  public int PaymentCount { get; set; }
        //   public int ClaimsCount { get; set; }

    }
}
