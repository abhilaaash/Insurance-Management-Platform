using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsuranceProject.Model
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "Customer First Name should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z ]+$", ErrorMessage = "Invalid characters in Customer First Name.")]
        public string CustomerFirstName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "Customer Last Name should not exceed 50 characters.")]
        [CustomRegex("^[a-zA-Z ]+$", ErrorMessage = "Invalid characters in Customer Last Name.")]
        public string CustomerLastName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [CustomEmail(ErrorMessage = "Invalid format.")]
        [StringLength(100, ErrorMessage = "Email should not exceed 100 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [CustomPhone(ErrorMessage = "Phone number should be a 10-digit number.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [CustomDOB(ErrorMessage = "Customer must be at least 18 years old.")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(100, ErrorMessage = "Address should not exceed 100 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "State should not exceed 50 characters.")]
        [CustomRegex("[a-zA-Z][a-zA-Z\\s]*$", ErrorMessage = "Invalid characters in State.")]
        public string State { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "City should not exceed 50 characters.")]
        [CustomRegex("[a-zA-Z][a-zA-Z\\s]*$", ErrorMessage = "Invalid characters in City.")]
        public string City { get; set; }


        public bool Status { get; set; }

        public Agent? Agent { get; set; }

        [ForeignKey("Agent")]
        public int? AgentId { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public Claim Claim { get; set; }
        public List<Complaint>? Queries { get; set; }
        public List<Document>? Documents { get; set; }
        public List<Policy>? Policies { get; set; }


    }
}
