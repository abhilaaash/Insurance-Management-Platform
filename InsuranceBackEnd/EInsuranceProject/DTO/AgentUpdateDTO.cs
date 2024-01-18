using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class AgentUpdateDTO
    {
        public int AgentId { get; set; }
        public string AgentFirstName { get; set; }
        public string AgentLastName { get; set; }
        public string Qualification { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? UserId { get; set; }
    }
}
