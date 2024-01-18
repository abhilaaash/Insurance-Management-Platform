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
    public class AgentController : ControllerBase
    {
        private IAgentService _agentService;
        private IUserService _userService;
        public AgentController(IAgentService agentService, IUserService userService)
        {
            _agentService = agentService;
            _userService = userService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult>GetAgents()
        {
            string[] innerTable = {"Customers"};
            var agents=await _agentService.GetAll(innerTable);
            var agentDTOS=new List<AgentShowDTO>();
            foreach (var agent in agents)
            {
                agentDTOS.Add(ConvertToAgentDTO(agent));
                

            }
            return Ok(agentDTOS);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByUserID([FromQuery] int Id)
        {
            
            string[] innerTables = { "Customers" };
            var agent = await _agentService.GetById(Id,innerTables);
            var agentDTO = ConvertToAgentDTO(agent);
            return Ok(agentDTO);
        }


        [HttpGet("Id")]
        public async Task<IActionResult> GetByAgentId([FromQuery] int Id)
        {

            string[] innerTables = { "Customers" };
            var agent = await _agentService.GetByAgentId(Id, innerTables);
            var agentDTO = ConvertToAgentDTO(agent);
            return Ok(agentDTO);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddAgentWithUser(AgentDTO adminDTO)
        {
            int id = -1;
            var existingUser = _userService.GetRoleNameForUser(adminDTO.Username);
            if (existingUser != null)
            {
                return BadRequest("Username Exist");
            }

            var user = ConvertToUser(adminDTO);
            var agent = ConvertToAgent(adminDTO);
            try
            {
                await _agentService.AddAgentWithUser(agent, user);
                return Ok(agent.AgentId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        //[HttpPost("AddAgent")]
        //public async Task<IActionResult> AddAgent([FromBody] AgentDTO agentDTO)
        //{
        //    var agent = ConvertToAgent(agentDTO);
        //    await _agentService.AddAgent(agent);
        //    return Ok(agent);
        //}


        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] AgentUpdateDTO agentDTO)
        {
            var newAgent = ConvertToAgentUpdate(agentDTO);
            await _agentService.UpdateAgent(newAgent);
            return Ok(newAgent.AgentId);
        }




        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            string[] innerTable = { "Customers" };
            Agent agent = await _agentService.GetByAgentId(id,innerTable);
            if (agent.Status == true)
            {
                agent.Status = false;
            }
            else
            {
                agent.Status=true;
            }

            //await _agentService.DeleteAgent(id);

          
            await _agentService.UpdateAgent(agent);
            return Ok(agent.AgentId);


        }
      

        private Agent ConvertToAgent(AgentDTO agentDTO)
        {
            var agent = new Agent()
            {
            //    AgentId = agentDTO.AgentId,
                AgentFirstName = agentDTO.AgentFirstName,
                AgentLastName = agentDTO.AgentLastName,
                Email = agentDTO.Email,
                Phone = agentDTO.Phone,
                UserId= null,
                
                Status = true,
              
                Customers = new List<Customer>(),
                Qualification=agentDTO.Qualification,

            
            };
            return agent;

        }
        private AgentShowDTO ConvertToAgentDTO(Agent agent)
        {
            var agentDTO = new AgentShowDTO()
            {
                AgentId = agent.AgentId,
                AgentFirstName = agent.AgentFirstName,
                AgentLastName = agent.AgentLastName,
                Email = agent.Email,
                Phone = agent.Phone,
                Qualification = agent.Qualification,
                CustomersCount = agent.Customers != null ? agent.Customers.Count() : 0,
                UserId = agent.UserId,
                status=agent.Status,
            };
            return agentDTO;
        }
        private Agent ConvertToAgentUpdate(AgentUpdateDTO agentDTO)
        {
            var agent = new Agent()
            {
                AgentId = agentDTO.AgentId,
                AgentFirstName = agentDTO.AgentFirstName,
                AgentLastName = agentDTO.AgentLastName,
                Email = agentDTO.Email,
                Phone = agentDTO.Phone,
                Qualification = agentDTO.Qualification,
                Status = true,
                UserId=agentDTO.UserId,
                

            };
            return agent;

        }
        private User ConvertToUser(AgentDTO adminDTO)
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
                RoleId = 3
                // Status = true,
            };
            return user;

        }
        [HttpGet("get")]

        public async Task<IActionResult> GetAgents([FromQuery] Parameter pageParameter)

        {

            string[] innerTable = { };

            var customers = await _agentService.GetAsync(pageParameter);

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
            string[] innerTable = { };
            if (user != null)
            {
                var customer = await _agentService.GetById(user.UserId,innerTable);
                return Ok(customer);
            }
            return BadRequest("Bad request ");
        }


    }
}
