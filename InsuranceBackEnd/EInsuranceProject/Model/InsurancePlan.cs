using EInsuranceProject.Validators;
using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.Model
{
    public class InsurancePlan
    {
        [Key]
        public int PlanId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "PlanName should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z][a-zA-Z\\s]*$", ErrorMessage = "Invalid characters in PlanName.")]
        public string PlanName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public bool Status { get; set; }

        public List<InsuranceScheme>? Schemes { get; set; }
    }
}
