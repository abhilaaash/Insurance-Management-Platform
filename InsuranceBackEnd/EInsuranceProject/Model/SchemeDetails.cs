using EInsuranceProject.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsuranceProject.Model
{
    public class SchemeDetails
    {

        [Key]
        [Required(ErrorMessage = "Should be same as schemeId")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]
        public int DetailId { get; set; }

        public string SchemeImage { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(200, ErrorMessage = "PlanName should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z][a-zA-Z\\s]*$", ErrorMessage = "Invalid characters in PlanName.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "MinAmount is required")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]
        public Int32 MinAmount { get; set; }

        [Required(ErrorMessage = "MaxAmount is required")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]
        public Int32 MaxAmount { get; set; }

        [Required(ErrorMessage = "MinAge is required")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]
        public int MinAge { get; set; }

        [Required(ErrorMessage = "MaxAge is required")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]
        public int MaxAge { get; set; }

        [Required(ErrorMessage = "MinTerm is required")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]
        public int MinTerm { get; set; }

        [Required(ErrorMessage = "MaxTerm is required")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]
        public int MaxTerm { get; set; }

        [Required(ErrorMessage = "ProfitPercent is required")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]
        public double ProfitPercent { get; set; }

        [Required(ErrorMessage = "FirstPremiumCommissionPercent is required")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]
        public double FirstPremiumCommissionPercent { get; set; }

        [Required(ErrorMessage = "EMICommissionPercent is required")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]
        public double EMICommissionPercent { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public bool Status { get; set; }

        public InsuranceScheme InsuranceScheme { get; set; }




    }
}
