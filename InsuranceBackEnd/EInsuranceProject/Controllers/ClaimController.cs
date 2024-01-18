using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.DTO;
using EInsuranceProject.Model;
using EInsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EInsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
      private  IClaimService _claimService;
        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetClaims()
        {
            string[] innerTable = {  };
            var Claims = await _claimService.GetAll(innerTable);
            var ClaimDTOS = new List<ClaimShowDTO>();
            foreach (var claim in Claims)
            {
                ClaimDTOS.Add(ConvertToClaimDTO(claim));
            }
            return Ok(ClaimDTOS);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByID([FromQuery] int Id)
        {

            var Claim = await _claimService.GetById(Id);
           
            return Ok(Claim);
        }
      

        [HttpPost("Add")]
        public async Task<IActionResult> AddClaim([FromBody] ClaimAddDTO claimDTO)
        {
            var Claim = ConvertToClaim(claimDTO);
            await _claimService.AddClaim(Claim);
            return Ok(Claim);
        }
        [HttpPut("UpdateClaim")]
        public async Task<IActionResult> Update([FromBody] ClaimDTO claimDTO)
        {
            var newClaim = ConvertToClaimUpdate(claimDTO);
            await _claimService.UpdateClaim(newClaim);
            return Ok(newClaim);
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _claimService.DeleteClaim(id);

            return Ok(id);


        }
        private Claim ConvertToClaim(ClaimAddDTO claimDTO)
        {
            var Claim = new Claim()
            {
                ClaimId = 0,
                ClaimAmount = claimDTO.ClaimAmount,
                ClaimDate = DateTime.Now,
                BankAccountNo = claimDTO.BankAccountNo,
               BankIFSCCode = claimDTO.BankIFSCCode,
                Status = false,
                PolicyNo=claimDTO.PolicyNo,
                CustomerId= claimDTO.CustomerId,

            };
            return Claim;

        }
        private Claim ConvertToClaimUpdate(ClaimDTO claimDTO)
        {
            var Claim = new Claim()
            {
                ClaimId = claimDTO.ClaimId,
                ClaimAmount = claimDTO.ClaimAmount,
                ClaimDate = claimDTO.ClaimDate,
                BankAccountNo = claimDTO.BankAccountNo,
                BankIFSCCode = claimDTO.BankIFSCCode,
                Status = true,
                PolicyNo = claimDTO.PolicyNo,
                CustomerId = claimDTO.CustomerId,

            };
            return Claim;

        }
        private ClaimShowDTO ConvertToClaimDTO(Claim claim)
        {
            var ClaimDTO = new ClaimShowDTO()
            {
                ClaimId = claim.ClaimId,
                ClaimAmount = claim.ClaimAmount,
                ClaimDate = claim.ClaimDate,
                BankAccountNo = claim.BankAccountNo,
                BankIFSCCode = claim.BankIFSCCode,
                status = claim.Status,
                PolicyNo= claim.PolicyNo,
                CustomerId = claim.CustomerId
                
            };
            return ClaimDTO;
        }


        [HttpGet("get")]

        public async Task<IActionResult> GetClaim([FromQuery] PageParameters pageParameter)

        {

            string[] innerTable = { };

            var customers = await _claimService.GetAsync(pageParameter);

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
