using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectInsurance.Model
{
    public class Commission
    {
        [Key]
        public int CommissionId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

        public bool status { get; set; }

        public Policy Policy { get; set; }
        [ForeignKey("Policy")]
        public int PolicyId { get; set; }

        public Agent Agent { get; set; }
        [ForeignKey("Agent")]
        
        public int AgentId { get; set; }
    }
}
