using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class InsuranceDetailsDTO
    {
        public int PlanId { get; set; }
        public string SchemeName { get; set; }
       
        public string SchemeImage { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Int32 MinAmount { get; set; }
        [Required]
        public Int32 MaxAmount { get; set; }
        [Required]
        public int MinAge { get; set; }
        [Required]
        public int MaxAge { get; set; }
        public int MinTerm { get; set; }
        public int MaxTerm { get; set; }

        public double ProfitPercent { get; set; }

        public double FirstPremiumCommissionPercent { get; set; }
        public double EMICommissionPercent { get; set; }
    }
}
