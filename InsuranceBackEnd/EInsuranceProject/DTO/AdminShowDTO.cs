using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class AdminShowDTO
    {
        public int AdminId { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public int? UserId { get; set; }
    }
}
