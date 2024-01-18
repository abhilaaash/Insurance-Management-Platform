using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class CustomerUpdateDTO
    {
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        public string? Address { get; set; }
        public string State { get; set; }
        public DateTime DOB { get; set; }
        public string? City { get; set; }
        public int? AgentId { get; set; }
        public int? UserId { get; set; }

    }
}
