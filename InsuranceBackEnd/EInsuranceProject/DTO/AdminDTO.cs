using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class AdminDTO
    {
      //  public int AdminId { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [StringLength(50, ErrorMessage = " First Name should not exceed 50 characters")]
        public string AdminFirstName { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [StringLength(50, ErrorMessage = " Last Name should not exceed 50 characters")]
        public string AdminLastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
      
        //public string Role { get; set; } = "Admin";
        //public bool Status { get;set; }
    }
}
