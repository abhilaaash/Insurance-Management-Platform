using EInsuranceProject.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace EInsuranceProject.Model
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [CustomPassword(ErrorMessage = "Invalid password format.")]
        public string Paasword { get; set; }

        public Role? Roles { get; set; }

        [ForeignKey("Roles")]
        [Required(ErrorMessage = "RoleId is required")]
        [CustomNonNegativeNumber(ErrorMessage = "RoleId must be non-negative.")]
        public int? RoleId { get; set; }

        public Agent Agents { get; set; }
        public Employee Employees { get; set; }
        public Admin Admins { get; set; }
        public Customer Customers { get; set; }
    }
}
