using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class InsurancePlanDTO
    {
        public int PlanId { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "PlanName should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z][a-zA-Z\\s]*$", ErrorMessage = "Invalid characters in PlanName.")]
        public string PlanName { get; set; }
        //   public bool Status { get; set; }

        //   public int  SchemesCount { get; set; }
    }
}
