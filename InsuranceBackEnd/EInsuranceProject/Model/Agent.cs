using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsuranceProject.Model
{
    public class Agent
    {
        [Key]
        public int AgentId { get; set; }

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

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
        public bool Status { get; set; }

        public List<Customer>? Customers { get; set; }
    }
}
