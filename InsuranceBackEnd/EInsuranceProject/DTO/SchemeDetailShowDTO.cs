using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class SchemeDetailShowDTO
    {
        public int DetailId { get; set; }
        public string SchemeImage { get; set; }
        public string Description { get; set; }
        public Int32 MinAmount { get; set; }
        public Int32 MaxAmount { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int MinTerm { get; set; }
        public int MaxTerm { get; set; }

        public double ProfitPercent { get; set; }

        public double FirstPremiumCommissionPercent { get; set; }
        public double EMICommissionPercent { get; set; }
    }
}
