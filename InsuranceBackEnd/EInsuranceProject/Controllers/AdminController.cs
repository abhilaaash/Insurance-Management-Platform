using EInsuranceProject.DTO;
using EInsuranceProject.Model;
using EInsuranceProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace EInsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminService _adminService;
        private IUserService _userService;
        private ICustomerService _customerService;
       
        public AdminController(IAdminService adminService, IUserService userService, ICustomerService customerService)
        {
            _adminService = adminService;
            _userService = userService;
            _customerService = customerService;
        }


        [HttpGet("Get")]
        public async Task<IActionResult> GetAdmins()
        {
            string[] innerTable = { };
            var admins = await _adminService.GetAll(innerTable);
            var adminDTOS = new List<AdminShowDTO>();
            foreach (var admin in admins)
            {
                adminDTOS.Add(ConvertToAdminDTO(admin));
            }
            return Ok(adminDTOS);
        }
       
        //[HttpPost("AddAdmin")]
        //public async Task<IActionResult> AddAdmin([FromBody] AdminDTO adminDTO)
        //{
        //    var Admin = ConvertToAdmin(adminDTO);
        //    await _adminService.AddAdmin(Admin);
        //    return Ok(Admin);
        //}
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByUserID([FromQuery] int Id)
        {

            var customer = await _adminService.GetById(Id);
            var customerDTO = ConvertToAdminDTO(customer);
            return Ok(customerDTO);
        }
        //[HttpGet("GetByUserId")]
        //public async Task<IActionResult> GetByUserID([FromQuery] int Id)
        //{
        //    string[] innerTable = { };
        //    var customer = await _adminService.GetByUserId(Id, innerTable);
        //    var customerDTO = ConvertToAdminDTO(customer);
        //    return Ok(customerDTO);
        //}




        [HttpPost("Add")]
        public async Task<IActionResult> AddAdminWithUser(AdminDTO adminDTO)
        {
            var existingUser = _userService.GetRoleNameForUser(adminDTO.Username);
            if (existingUser != null)
            {
                return BadRequest(-1);
            }

            var user = ConvertToUser(adminDTO);
            var admin = ConvertToAdmin(adminDTO);
            try
            {
                await _adminService.AddAdminWithUser(admin, user);
                return Ok(admin.AdminId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] AdminUpdateDTO adminDTO)
        {
            var newAdmin = ConvertToAdminUpdate(adminDTO);
            await _adminService.UpdateAdmin(newAdmin);
            return Ok(  newAdmin.AdminId);
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _adminService.DeleteAdmin(id);

            return Ok(id);


        }

      

        private Admin ConvertToAdmin(AdminDTO adminDTO)
        {
            // string passwordHash = BCrypt.Net.BCrypt.HashPassword(adminDTO.Password);
            var Admin = new Admin()
            {
                //   AdminId=adminDTO.AdminId,
                AdminFirstName = adminDTO.AdminFirstName,
                AdminLastName = adminDTO.AdminLastName,
                UserId=null,
                
                //  Username=adminDTO.Username,
                //  Password= passwordHash,
                Status = true,
            };
            return Admin;

        }
        private Admin ConvertToAdminUpdate(AdminUpdateDTO adminDTO)
        {
            var Admin = new Admin()
            {
                AdminId = adminDTO.AdminId,
                AdminFirstName = adminDTO.AdminFirstName,
                AdminLastName = adminDTO.AdminLastName,
                UserId=adminDTO.UserId,
                Status = true
            };
            return Admin;

        }
        private AdminShowDTO ConvertToAdminDTO(Admin admin)
        {
            var adminDTO = new AdminShowDTO()
            {
                AdminId = admin.AdminId,
                AdminFirstName = admin.AdminFirstName,
                AdminLastName = admin.AdminLastName,
                UserId = admin.UserId,
                
            };
            return adminDTO;
        }

        private User ConvertToUser(AdminDTO adminDTO)
        {
             string passwordHash = BCrypt.Net.BCrypt.HashPassword(adminDTO.Password);
            var user = new User()
            {
                 Username=adminDTO.Username,
                 Paasword= passwordHash,
                 RoleId=1
            };
            return user;

        }


    }
}
