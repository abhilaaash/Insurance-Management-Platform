using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.DTO;
using EInsuranceProject.Model;
using EInsuranceProject.Services;
using EInsuranceProject.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EInsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        private IUserService _userService;
        private IDocumentService _documentService;
        private readonly InsuranceContext _context;

        public CustomerController(ICustomerService customerService, IUserService userService, IDocumentService documentService, InsuranceContext context)
        {
            _customerService = customerService;
            _userService = userService;
            _documentService = documentService;
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetCustomers()
        {
            string[] innerTable = { "Documents", "Policies", "Queries" };
            var customers = await _customerService.GetAll(innerTable);
            var customerDTOS = new List<CustomerShowDTO>();
            foreach (var customer in customers)
            {
                customerDTOS.Add(ConvertToCustomerDTO(customer));
            }
            return Ok(customerDTOS);
        }
        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetByID([FromQuery] int Id)
        {
            string[] innerTable = { "Documents", "Policies", "Queries" };
            var customer = await _customerService.GetByUserId(Id,innerTable);
            var customerDTO = ConvertToCustomerDTO(customer);
            return Ok(customerDTO);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByCustomerId([FromQuery] int Id)
        {
            string[] innerTable = { "Documents", "Policies", "Queries" };
            var customer = await _customerService.GetByCustomerId(Id, innerTable);
            var customerDTO = ConvertToCustomerDTO(customer);
            return Ok(customerDTO);
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddCustomerWithUser(CustomerDTO adminDTO)
        {
           
            var existingPhone = _customerService.GetByPhone(adminDTO.Phone);
            if (existingPhone != null)
            {
                return BadRequest("Phone exist");
            }
            var existingEmail = _customerService.GetByEmail(adminDTO.Email);
            if (existingEmail != null)
            {
                return BadRequest("Email exist");
            }


            var existingUser = _userService.GetRoleNameForUser(adminDTO.Username);
            if (existingUser != null)
            {
                return BadRequest("Username exist");
            }

            var user = ConvertToUser(adminDTO);
            var customer = ConvertToCustomer(adminDTO);
            try
            {
                await _customerService.AddCustomerWithUser(customer, user);
                return Ok(customer.CustomerId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        //[HttpPost("AddCustomer")]
        //public async Task<IActionResult> AddCustomer([FromBody] CustomerDTO customerDTO)
        //{
        //    var customer = ConvertToCustomer(customerDTO);
        //    await _customerService.AddCustomer(customer);
        //    return Ok(customer);
        //}


        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] CustomerUpdateDTO customerDTO)
        {
            var newCustoemr = ConvertToCustomerUpdate(customerDTO);
            await _customerService.UpdateCustomer(newCustoemr);
            return Ok(newCustoemr.CustomerId);
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            string[] innerTable = { "Documents", "Policies", "Queries" };
            Customer customer = await _customerService.GetByCustomerId(id, innerTable);
            if (customer.Status == true)
            {
                customer.Status = false;
            }
            else
            {
                customer.Status = true;
            }
            await _customerService.UpdateCustomer(customer);
            // await _customerService.DeleteCustomer(id);

            return Ok(id);
        }
        private Customer ConvertToCustomer(CustomerDTO customerDTO)
        {
            var customer = new Customer()
            {
                //   CustomerId = customerDTO.CustomerId,
                CustomerFirstName = customerDTO.CustomerFirstName,
                CustomerLastName = customerDTO.CustomerLastName,
                Email = customerDTO.Email,
                Phone = customerDTO.Phone,
                State = customerDTO.State,
                City = customerDTO.City,
                DOB=customerDTO.DOB,
                UserId = null,
               
              AgentId = customerDTO.AgentId == 0 ? null : customerDTO.AgentId,
            //  AgentId=customerDTO.AgentId,
                Status = true,
                Address = customerDTO.Address,
                Documents = new List<Document>(),
                Policies = new List<Policy>(),
                Queries = new List<Complaint>(),

            };
            return customer;

        }
        private CustomerShowDTO ConvertToCustomerDTO(Customer customer)
        {
            int documentCount = 0; if (customer.Documents != null)
            {
                foreach (var cust in customer.Documents)
                {
                    if (cust.status == true)
                    {
                        documentCount++;
                    }
                }
            }
            var customerDTO = new CustomerShowDTO()
            {
                CustomerId = customer.CustomerId,
                CustomerFirstName = customer.CustomerFirstName,
                CustomerLastName = customer.CustomerLastName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                State = customer.State,
                City = customer.City,
                UserId = customer.UserId,
                DOB = customer.DOB,
                AgentId = customer.AgentId == 0 ? null : customer.AgentId,          // AgentId = null,
                DocumentsCount = documentCount,         //  DocumentsCount = customer.Documents != null?customer.Documents.Count() : 0,
                PoliciesCount = customer.Policies != null ? customer.Policies.Count() : 0,
                QueryCount = customer.Queries != null ? customer.Queries.Count() : 0,
            }; return customerDTO;
        }
        private Customer ConvertToCustomerUpdate(CustomerUpdateDTO customer)
        {
            var customerDTO = new Customer()
            {
                CustomerId = customer.CustomerId,
                CustomerFirstName = customer.CustomerFirstName,
                CustomerLastName = customer.CustomerLastName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                State = customer.State,
                City = customer.City,
                DOB= customer.DOB,
                UserId=customer.UserId,
            //    AgentId = customer.AgentId == 0 ? null : customer.AgentId,
                AgentId=customer.AgentId!=null?customer.AgentId:null,
                Status = true
            };
            return customerDTO;
        }


        [HttpGet("GetByAgentId/{agentId}")]
        public async Task<IActionResult> GetCustomersByAgentIdAsync(int agentId)
        {
            var customers = await _customerService.GetCustomersByAgentIdAsync(agentId);
            return Ok(customers);
        }
       

        private User ConvertToUser(CustomerDTO customerDTO)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(customerDTO.Password);
            var user = new User()
            {
                //   AdminId=adminDTO.AdminId,
                // AdminFirstName = adminDTO.AdminFirstName,
                //AdminLastName = adminDTO.AdminLastName,
                //UserId = null,

                Username = customerDTO.Username,
                Paasword = passwordHash,
                RoleId = 4
                // Status = true,
            };
            return user;
        }
       

       [HttpGet("get")]

        public async Task<IActionResult> GetCustomers([FromQuery] Parameter pageParameter)

        {

            string[] innerTable = { };

            var customers = await _customerService.GetAsync(pageParameter);

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


        [HttpGet("getProfile")]
        public async Task<IActionResult> GetByUserNameAsync(string name)
        {
            var user = await _userService.FindUser(name);

            if (user != null )
            {
                var customer = await _customerService.GetCustomerByUserIDAsync(user.UserId);
                return Ok(customer);
            }
            return BadRequest("Bad request ");
        }
        [HttpGet("policies")]
        public async Task<IActionResult> GetPolicies([FromQuery] Parameter filterParameter, string userName)
        {
            var user = await _userService.FindUser(userName);

            if (user != null && user.RoleId == 4)
            {
                var policies = await _customerService.GetCustomerWithPolicy(user.UserId, filterParameter);

                var metadata = new
                {
                    policies.TotalCount,
                    policies.PageSize,
                    policies.CurrentPage,
                    policies.TotalPages,
                    policies.HasNext,
                    policies.HasPrevious,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(policies);
            }

            return BadRequest("Bad request ");
        }

        [HttpGet("documents")]
        public async Task<IActionResult> GetDocuments([FromQuery] PageParameters pageParameter, int customerId)
        {
            var documents = await _documentService.GetByCustomerIdAsync(pageParameter, customerId);
            if (documents != null)
            {
                var metadata = new
                {
                    documents.TotalCount,
                    documents.PageSize,
                    documents.CurrentPage,
                    documents.TotalPages,
                    documents.HasNext,
                    documents.HasPrevious,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(documents);
            }
            return BadRequest("Bad request");
        }



       
    }
}
