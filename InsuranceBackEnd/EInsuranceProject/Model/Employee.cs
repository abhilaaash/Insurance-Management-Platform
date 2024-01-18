using EInsuranceProject.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsuranceProject.Model
{
    public class Employee
    {

        [Key]
        public int EmployeeId { get; set; }

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

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        //[Required(ErrorMessage = "This Field is required.")]
        //[CustomDOB(ErrorMessage = "Employee must be at least 18 years old.")]
        //public DateTime? DateOfBirth { get; set; }
        //[Required(ErrorMessage = "This Field is required.")]
        public bool Status { get; set; }



    }
}
