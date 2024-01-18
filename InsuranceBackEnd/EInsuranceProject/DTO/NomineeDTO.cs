namespace EInsuranceProject.DTO
{
    public class NomineeDTO
    {
        
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "NominieName should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z][a-zA-Z\\s]*$", ErrorMessage = "Invalid characters in NominieName.")]
        public string NomineeName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "NominieRelation should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z][a-zA-Z\\s]*$", ErrorMessage = "Invalid characters in NominieRelation.")]
        public string Relation { get; set; }
    }
}
