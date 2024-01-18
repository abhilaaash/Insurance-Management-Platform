using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsuranceProject.DTO
{
    public class NominieDTO
    {
        public int NominieId { get; set; }
        

        public int PolicyNo { get; set; }
        public bool status { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "NominieName should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z][a-zA-Z\\s]*$", ErrorMessage = "Invalid characters in NominieName.")]
        public string NominieName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "NominieRelation should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z][a-zA-Z\\s]*$", ErrorMessage = "Invalid characters in NominieRelation.")]
        public string NominieRelation { get; set; }
    }
}
