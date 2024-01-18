using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class InsuranceSchemeDTO
    {
        //  public int SchemeId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "SchemeName should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z][a-zA-Z\\s]*$", ErrorMessage = "Invalid characters in SchemeName.")]
        public string SchemeName { get; set; }
        //   public bool Status { get; set; }

        //    public int PoliciesCount { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [CustomNonNegativeNumber(ErrorMessage = "InsurancePlanId must be non-negative.")]
        public int InsurancePlanId { get; set; }
    }
}
