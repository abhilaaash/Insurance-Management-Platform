using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class PolicyShowDTO
    {
        public int PolicyNo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public double Premium { get; set; }
        public double SumAssured { get; set; }
        public Mode PremiumMode { get; set; }

        public int TotalPremiumNo { get; set; }
        public bool Status { get; set; }
        public int CustomerId { get; set; }
        public int? AgentId { get; set; } = 0;
        public int SchemeId { get; set; }
        public int NomineeCount { get; set; }
        public int PaymentCount { get; set; }
        public int ClaimsCount { get; set; }
    }
}
