using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class AgentDTO
    {
        [Required(ErrorMessage = "This Field is required.")]
        [StringLength(50, ErrorMessage = "First Name should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z ]+$", ErrorMessage = "Invalid characters in Agent First Name.")]
        public string AgentFirstName { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [StringLength(50, ErrorMessage = "Agent Last Name should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z ]+$", ErrorMessage = "Invalid characters in Agent Last Name.")]
        public string AgentLastName { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [StringLength(50, ErrorMessage = "Qualification should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z ]+$", ErrorMessage = "Invalid characters in Agent Qualification.")]
        public string Qualification { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        [CustomEmail(ErrorMessage = "Invalid format for Email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This Field is required.")]
        [CustomPhone(ErrorMessage = "Phone number should be a 10-digit number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [CustomPassword(ErrorMessage = "Invalid password format.")]
        public string Password { get; set; }
        //  public string Role { get; set; } = "Agent";
        //    public Double CommissionEarned { get; set; }
        //    public bool Status { get; set; }
        //    public int CustomersCount { get; set; }
        //  public List<Customer> Customers{ get; set; }




    }
}
