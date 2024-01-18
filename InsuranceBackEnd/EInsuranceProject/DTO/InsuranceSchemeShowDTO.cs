using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class InsuranceSchemeShowDTO
    {
        public int SchemeId { get; set; }
        public string SchemeName { get; set; }
        public int PlanId { get; set; }
        public int PoliciesCount { get; set; }

        public bool status { get; set; }
       
    }
}
