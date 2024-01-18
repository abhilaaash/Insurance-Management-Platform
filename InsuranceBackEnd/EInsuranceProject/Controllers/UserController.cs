using EInsuranceProject.DTO;
using EInsuranceProject.Model;
using EInsuranceProject.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Claim = System.Security.Claims.Claim;

namespace EInsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService responseService, IConfiguration configuration)
        {
            _userService = responseService;
            _configuration = configuration;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetUser()
        {
            string[] innerTable = { "Roles"};
            var responses = await _userService.GetAll(innerTable);
            var responseDTOS = new List<UserShowDTO>();
            foreach (var response in responses)
            {
                responseDTOS.Add(ConvertToUserDTO(response));
            }
            return Ok(responseDTOS);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByID([FromQuery] int Id)
        {

            var response = await _userService.GetById(Id);
            var responseDTO = ConvertToUserDTO(response);
            return Ok(responseDTO);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddResponse([FromBody] UserDTO responseDTO)
        {
            var response = ConvertToUser(responseDTO);
            await _userService.AddUser(response);
            return Ok(response);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UserDTO responseDTO)
        {
            var newResponse = ConvertToUser(responseDTO);
            await _userService.UpdateUser(newResponse);
            return Ok("Updated Successfully");
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteUser(id);

            return Ok("RemovedSuccessfully");

        }
        private User ConvertToUser(UserDTO responseDTO)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(responseDTO.Paasword);
            var response = new User()
            {
               Username=responseDTO.Username,
               Paasword=passwordHash,
               RoleId=responseDTO.RoleId,

            };
            return response;

        }
        private UserShowDTO ConvertToUserDTO(User response)
        {
            var responseDTO = new UserShowDTO()
            {
               UserId=response.UserId,
               Username = response.Username,
               Paasword=response.Paasword,
               RoleId = response.RoleId
               

            };
            return responseDTO;
        }
        [HttpGet("user/role/{username}")]
        public IActionResult GetUserRoleName(string username)
        {
            var roleName = _userService.GetRoleNameForUser(username);

            if (roleName == null)
            {
                return NotFound("Role not found for the specified user.");
            }

            return Ok(roleName);
        }
        [HttpGet("user/roleId/{username}")]
        public IActionResult GetUserRoleId(string username)
        {
            var roleName = _userService.GetRoleIdForUser(username);

            if (roleName == null)
            {
                return NotFound("Role ID not found for the specified user.");
            }

            return Ok(roleName);
        }
        [HttpGet("user/userId/{username}")]
        public IActionResult GetUserId(string username)
        {
            var userId = _userService.GetUserIdForUser(username);

            if (userId == null)
            {
                return NotFound("user ID not found for the specified user.");
            }

            return Ok(userId);
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDTO userDTO)
        {
            // var user = _customerService.FindUser(customerDTO.Username);
            var user = _userService.FindUserAsync(c => c.Username == userDTO.Username);
            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(userDTO.Password, user.Paasword))
                {
                    var jwt = CreateToken(user);
                    return Ok(new Token() { ActualToken=jwt});
                }
            }
            return BadRequest("Username/password doesnt match");
        }

        private string CreateToken(User user)
        {
            var role = _userService.GetRoleNameForUser(user.Username);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,role),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        [HttpPut("update-password")]
        public IActionResult UpdatePassword([FromBody] PasswordDTO userPasswordDTO)
        {
            var success = _userService.UpdatePassword(userPasswordDTO.Username, userPasswordDTO.NewPassword);

            if (success)
            {
                return Ok();
            }
            else
            {
                return NotFound($"User with username {userPasswordDTO.Username} not found.");
            }
        }
    }



}
