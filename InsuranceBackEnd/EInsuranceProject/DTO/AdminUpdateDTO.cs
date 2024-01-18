using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class AdminUpdateDTO
    {
        public int AdminId { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public int? UserId { get; set; }
    }
}
