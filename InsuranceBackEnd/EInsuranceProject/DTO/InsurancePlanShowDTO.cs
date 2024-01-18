using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class InsurancePlanShowDTO
    {
         public int PlanId { get; set; }
        public string PlanName { get; set; }
        public bool status { get; set; }
        public List<InsuranceScheme> SchemesCount { get; set; }
    }
}
