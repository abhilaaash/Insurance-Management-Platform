using EInsuranceProject.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsuranceProject.Model
{
    public class InsuranceScheme
    {
        [Key]
        public int SchemeId { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "SchemeName should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z][a-zA-Z\\s]*$", ErrorMessage = "Invalid characters in SchemeName.")]
        public string SchemeName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public bool Status { get; set; }


        public SchemeDetails SchemeDetails { get; set; }


        public List<Policy>? Policies { get; set; }
        [ForeignKey("InsurancePlanId")]
        public InsurancePlan Plans { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [CustomNonNegativeNumber(ErrorMessage = "InsurancePlanId must be non-negative.")]
        public int InsurancePlanId { get; set; }


    }
}
