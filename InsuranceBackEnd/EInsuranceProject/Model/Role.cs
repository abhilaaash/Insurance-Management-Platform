using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.Model
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string RoleName { get; set; }


        public List<User> Users { get; set; }
    }
}
