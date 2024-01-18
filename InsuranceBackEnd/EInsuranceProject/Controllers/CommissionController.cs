
using EInsuranceProject.DTO;
using FinalProjectInsurance.DTO;
using FinalProjectInsurance.Model;
using FinalProjectInsurance.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectInsurance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommissionController : ControllerBase
    {
        private ICommissionService _commissionService;
        public CommissionController(ICommissionService commissionService)
        {
            _commissionService = commissionService;
        }
        [HttpGet("Get")]
        public async Task<IActionResult> GetCommission()
        {
            string[] innerTable = { };
            var commissions = await _commissionService.GetAll(innerTable);
            var commissionsDTOS = new List<ComissionShowDTO>();
            foreach (var commission in commissions)
            {
                commissionsDTOS.Add(ConvertToCommissionDTO(commission));
            }
            return Ok(commissionsDTOS);
        }  
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByID([FromQuery] int Id)
        {

            var commission = await _commissionService.GetById(Id);
            var commissionDTOs = ConvertToCommissionDTO(commission);
            return Ok(commissionDTOs);                                                  // Commission  CommissonDTO commission
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddCommission([FromBody] CommissionDTO commissionDTO)
        {
            var commission = ConvertToCommission(commissionDTO);
            await _commissionService.AddCommission(commission);
            return Ok(commission);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCommission([FromBody] CommissionDTO commissionDTO)
        {
            var newClaim = ConvertToCommission(commissionDTO);
            await _commissionService.UpdateCommission(newClaim);
            return Ok(newClaim);
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteClaim(int id)
        {
            await _commissionService.DeleteCommission(id);

            return Ok();


        }
        private Commission ConvertToCommission(CommissionDTO commissionDTO)
        {
            var commission = new Commission()
            {
              CommissionId=commissionDTO.CommissionId,
              Amount=commissionDTO.Amount,
              Date=DateTime.Now,
              AgentId=commissionDTO.AgentId,
              PolicyId = commissionDTO.PolicyId,
              status=commissionDTO.status
              
            };
            return commission;

        }
        private ComissionShowDTO ConvertToCommissionDTO(Commission commission)
        {
            var commissionDTO = new ComissionShowDTO()
            {
                CommissionId = commission.CommissionId,
                Amount = commission.Amount,
                Date = DateTime.Now,
                AgentId = commission.AgentId,
                PolicyId = commission.PolicyId,
                status = commission.status
            };
            return commissionDTO;
        }

        [HttpGet("GetByPolicyId/{policyId}")]
        public async Task<IActionResult> GetCommissionByPolicyIdAsync(int policyId)
        {
            var commission = await _commissionService.GetCommissionByPolicyIdAsync(policyId);
            return Ok(commission);
        }

        [HttpGet("GetByAgentId/{agentId}")]
        public async Task<IActionResult> GetCommissionsByAgentIdAsync(int agentId)
        {
            var commissions = await _commissionService.GetCommissionsByAgentIdAsync(agentId);
            return Ok(commissions);
        }
    }
}
