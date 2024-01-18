using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class PolicyUpdateDTO
    {
        public int PolicyNo { get; set; }
       // public DateTime IssueDate { get; set; }
        [Required]
        public DateTime MaturityDate { get; set; }
        //    [Required]
        //    public Mode PremiumMode { get; set; }
        [Required]
        public double Premium { get; set; }
        [Required]
        public double SumAssured { get; set; }
        public int SchemeId { get; set; }
        //   public bool Status { get; set; }
        public int CustomerId { get; set; }
        public int? AgentId { get; set; } = 0;
    }
}
