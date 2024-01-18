using EInsuranceProject.DTO;
using EInsuranceProject.Model;
using EInsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EInsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancePlansController : ControllerBase
    {
        private IInsurancePlanService _insurancePlanService;

        public InsurancePlansController(IInsurancePlanService insurancePlanService) 
        {
            _insurancePlanService = insurancePlanService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetInsurancePlans()
        {
            string[] innerTable = { "Schemes" };
            var Plans = await _insurancePlanService.GetAll(innerTable);
            var PlanDTOS = new List<InsurancePlanShowDTO>();
            foreach (var plan in Plans)
            {
                PlanDTOS.Add(ConvertToPlanDTO(plan));
            }
            return Ok(PlanDTOS);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByID([FromQuery] int Id)
        {
            string[] innerTable = { "Schemes" };
            var Plan = await _insurancePlanService.GetById(Id,innerTable);
            var PlanDTO = ConvertToPlanDTO(Plan);
            return Ok(PlanDTO);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddInsurancPlan([FromBody] InsurancePlanDTO planDTO)
        {
            string[] innerTable = { };
            InsurancePlan planCheck = await _insurancePlanService.CheckPlaneName(planDTO.PlanName, innerTable);
            if (planCheck !=null)
            {
                throw new Exception("Plan already exist");
            }
            else
            {
                var plan = ConvertToInsurancePlan(planDTO);
                await _insurancePlanService.AddInsurancePlan(plan);
                return Ok(plan);
            }
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] InsurancePlanDTO planDTO)
        {
            var newPlan = ConvertToInsurancePlan(planDTO);
            await _insurancePlanService.UpdatePlan(newPlan);
            return Ok(newPlan.PlanId);
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            string[] innerTable = { "Schemes" };
            InsurancePlan plan = await _insurancePlanService.GetById(id, innerTable);
            if (plan.Status == true)
            {
                plan.Status = false;
            }
            else
            {
                plan.Status = true;
            }
            await _insurancePlanService.UpdatePlan(plan);

            //await _insurancePlanService.DeletePlan(id);

            return Ok(id);


        }

        private InsurancePlan ConvertToInsurancePlan(InsurancePlanDTO insurancePlanDTO)
        {
            var insurancePlan = new InsurancePlan()
            {
          //      PlanId = insurancePlanDTO.PlanId,
                PlanName = insurancePlanDTO.PlanName,

                Schemes = new List<InsuranceScheme>(),
               
                Status=true,
            };
            return insurancePlan;

        }
        private InsurancePlanShowDTO ConvertToPlanDTO(InsurancePlan insurancePlan)
        {
            var PlanDTO = new InsurancePlanShowDTO()
            {
               PlanId = insurancePlan.PlanId,
                PlanName = insurancePlan.PlanName,

                status = insurancePlan.Status,
              
               SchemesCount = insurancePlan.Schemes
            };
            return PlanDTO;
            
        }
    }
}
