using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.DTO;
using EInsuranceProject.Model;
using EInsuranceProject.Services;
using EInsuranceProject.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EInsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;
        private IUserService _userService;
        public EmployeeController(IEmployeeService employeeService, IUserService userService)
        {
            _employeeService = employeeService;
            _userService = userService;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetEmployees()
        {
            string[] innerTable = { };
            var Employees = await _employeeService.GetAll(innerTable);
            var EmployeeDTOS = new List<EmployeeShowDTO>();
            foreach (var employee in Employees)
            {
                EmployeeDTOS.Add(ConvertToEmployeeDTO(employee));
            }
            return Ok(EmployeeDTOS);
        }
        [HttpGet("GetByUSerId")]
        public async Task<IActionResult> GetByUserID([FromQuery] int Id)
        {

            var customer = await _employeeService.GetById(Id);
            var customerDTO = ConvertToEmployeeDTO(customer);
            return Ok(customerDTO);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByID([FromQuery] int Id)
        {

            var customer = await _employeeService.GetById(Id);
            var customerDTO = ConvertToEmployeeDTO(customer);
            return Ok(customerDTO);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddAdminWithUser(EmployeeDTO adminDTO)
        {
            var existingUser = _userService.GetRoleNameForUser(adminDTO.Username);
            if (existingUser != null)
            {
                return BadRequest("Username Exist");
            }

            var user = ConvertToUser(adminDTO);
            var employee = ConvertToEmployee(adminDTO);
            try
            {
                await _employeeService.AddEmployeeWithUser(employee, user);
                return Ok(employee.EmployeeId );
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        //[HttpPost("AddEmployee")]
        //public async Task<IActionResult> AddEmployee([FromBody] EmployeeDTO employeeDTO)
        //{
        //    var customer = ConvertToEmployee(employeeDTO);
        //    await _employeeService.AddEmployee(customer);
        //    return Ok(customer);
        //}
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] EmployeeUpdateDTO employeeDTO)
        {
            var newEmployee = ConvertToEmployeeUpdate(employeeDTO);
            await _employeeService.UpdateEmployee(newEmployee);
            return Ok(newEmployee.EmployeeId);
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {

            string[] innerTable = { };
            Employee employee = await _employeeService.GetByEmployeeId(id);
            if (employee.Status == true)
            {
                employee.Status = false;
            }
            else
            {
                employee.Status = true;
            }
            await _employeeService.UpdateEmployee(employee);
          //  await _employeeService.DeleteEmployee(id);

            return Ok(id);


        }
        private Employee ConvertToEmployee(EmployeeDTO employeeDTO)
        {
         //   string passwordHash = BCrypt.Net.BCrypt.HashPassword(employeeDTO.Password);
            var Employee = new Employee()
            {
              //  EmployeeId= employeeDTO.EmployeeId,
                EmployeeFirstName= employeeDTO.EmployeeFirstName,
                EmployeeLastName= employeeDTO.EmployeeLastName,
                Email= employeeDTO.Email,
                Phone= employeeDTO.Phone,
                Salary= employeeDTO.Salary,
                UserId= null,
           //     Username=employeeDTO.Username,
             //   Password=passwordHash,
                Status= true,
               

            };
            return Employee;

        }
        private EmployeeShowDTO ConvertToEmployeeDTO(Employee employee)
        {
            var EmployeeDTO = new EmployeeShowDTO()
            {
                EmployeeId = employee.EmployeeId,
                EmployeeFirstName = employee.EmployeeFirstName,
                EmployeeLastName = employee.EmployeeLastName,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary=employee.Salary,
                UserId=employee.UserId,

            //    Username=employee.Username,
             //   Password=employee.Password,
                status = employee.Status,
               

            };
            return EmployeeDTO;
        }

        private Employee ConvertToEmployeeUpdate(EmployeeUpdateDTO employeeDTO)
        {
            //   string passwordHash = BCrypt.Net.BCrypt.HashPassword(employeeDTO.Password);
            var Employee = new Employee()
            {
                EmployeeId= employeeDTO.EmployeeId,
                EmployeeFirstName = employeeDTO.EmployeeFirstName,
                EmployeeLastName = employeeDTO.EmployeeLastName,
                Email = employeeDTO.Email,
                Phone = employeeDTO.Phone,
                Salary = employeeDTO.Salary,
                UserId=employeeDTO.UserId,
                Status = true

            };
            return Employee;

        }
        private User ConvertToUser(EmployeeDTO adminDTO)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(adminDTO.Password);
            var user = new User()
            {
                //   AdminId=adminDTO.AdminId,
                // AdminFirstName = adminDTO.AdminFirstName,
                //AdminLastName = adminDTO.AdminLastName,
                //UserId = null,

                Username = adminDTO.Username,
                Paasword = passwordHash,
                RoleId = 2
                // Status = true,
            };
            return user;

        }
        [HttpGet("get")]

        public async Task<IActionResult> GetEmployee([FromQuery] Parameter pageParameter)

        {

            string[] innerTable = { };

            var customers = await _employeeService.GetAsync(pageParameter);

            var metadata = new

            {

                customers.TotalCount,

                customers.PageSize,

                customers.CurrentPage,

                customers.TotalPages,

                customers.HasNext,

                customers.HasPrevious,

            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));



            return Ok(customers);

        }



    }
}
