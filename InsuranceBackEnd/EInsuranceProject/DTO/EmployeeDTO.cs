using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class EmployeeDTO
    {
        //   public int EmployeeId { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [StringLength(50, ErrorMessage = "First Name should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z ]+$", ErrorMessage = "Invalid characters in First Name.")]
        public string EmployeeFirstName { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [StringLength(50, ErrorMessage = "Last Name should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z ]+$", ErrorMessage = "Invalid characters in Last Name.")]
        public string EmployeeLastName { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [CustomEmail(ErrorMessage = "Invalid format for Email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This Field is required.")]
        [CustomPhone(ErrorMessage = "Phone number should be a 10-digit number.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "This Field is required.")]
        [CustomNonNegativeNumber(ErrorMessage = "Salary must be non-negative.")]
        public Double? Salary { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [CustomPassword(ErrorMessage = "Invalid password format.")]
        public string Password { get; set; }
        //     public string Role { get; set; } = "Employee";
        //   public bool Status { get; set; }  
        //  public int UserId { get; set; }

    }
}
