using EInsuranceProject.Validators;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsuranceProject.Model
{
    public class Nominie
    {
        public int NominieId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "NominieName should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z][a-zA-Z\\s]*$", ErrorMessage = "Invalid characters in NominieName.")]
        public string NominieName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "NominieRelation should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z][a-zA-Z\\s]*$", ErrorMessage = "Invalid characters in NominieRelation.")]
        public string NominieRelation { get; set; }

        public Policy Policy { get; set; }
        [ForeignKey("Policy")]
        [Required(ErrorMessage = "This field is required.")]
        [CustomNonNegativeNumber(ErrorMessage = "PolicyNo must be non-negative.")]
        public int PolicyNo { get; set; }


        [Required(ErrorMessage = "This field is required.")]
        public bool Status { get; set; }
    }
}
