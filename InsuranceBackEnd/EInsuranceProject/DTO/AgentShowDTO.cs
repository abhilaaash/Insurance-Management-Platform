using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class AgentShowDTO
    {
        public int AgentId { get; set; }
        public string AgentFirstName { get; set; }
        public string AgentLastName { get; set; }
        public string Qualification { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int CustomersCount { get; set; }
        public bool status { get; set; }
        public int? UserId { get; set; }
    }
}
