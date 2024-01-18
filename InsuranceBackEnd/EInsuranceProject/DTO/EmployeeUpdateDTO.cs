using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class EmployeeUpdateDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Double? Salary { get; set; }
        public int? UserId { get; set; }
    }
}
