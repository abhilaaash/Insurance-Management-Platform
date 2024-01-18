using EInsuranceProject.DTO;
using EInsuranceProject.Model;
using EInsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EInsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceSchemeController : ControllerBase
    {
        private IInsuranceSchemesService _insuranceSchemesService;
        public InsuranceSchemeController(IInsuranceSchemesService insuranceSchemesService)
        {
            _insuranceSchemesService = insuranceSchemesService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetSchemes()
        {
            string[] innerTable = {  "SchemeDetails" };
            var Schemes = await _insuranceSchemesService.GetAll(innerTable);
            var SchemeDTOS = new List<InsuranceSchemeShowDTO>();
            foreach (var scheme in Schemes)
            {
                SchemeDTOS.Add(ConvertToSchemeDTO(scheme));
            }
            return Ok(SchemeDTOS);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByID([FromQuery] int Id)
        {

            var Scheme = await _insuranceSchemesService.GetById(Id);
            var SchemeDTO = ConvertToSchemeDTO(Scheme);
            return Ok(SchemeDTO);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddScheme([FromBody] InsuranceSchemeOnlyDTO schemeDTO)
        {
            var Scheme = ConvertToSchemeOnly(schemeDTO);
            await _insuranceSchemesService.AddScheme(Scheme);
            return Ok(Scheme);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] InsuranceDetailsDTO schemeDTO)
        {
            var NewScheme = ConvertToScheme(schemeDTO);
            await _insuranceSchemesService.UpdateScheme(NewScheme);
            return Ok("Updated Successfully");
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _insuranceSchemesService.DeleteScheme(id);

            return Ok("Removed Successfully");


        }
        [HttpGet("GetByPlanId/{planId}")]
        public async Task<IActionResult> GetInsuranceSchemeByPlanIdAsync(int planId)
        {
            var schemes = await _insuranceSchemesService.GetInsuranceSchemeByPlanIdAsync(planId);

            return Ok(schemes);
        }
        private InsuranceScheme ConvertToScheme(InsuranceDetailsDTO schemeDTO)
        {
            var Scheme = new InsuranceScheme()
            {
           //     SchemeId = schemeDTO.SchemeId,
                SchemeName = schemeDTO.SchemeName,
                Status = true,
                InsurancePlanId = schemeDTO.PlanId,
                Policies = new List<Policy>(),

                
            };
            return Scheme;

        }


        private InsuranceScheme ConvertToSchemeOnly(InsuranceSchemeOnlyDTO schemeDTO)
        {
            var Scheme = new InsuranceScheme()
            {
                //     SchemeId = schemeDTO.SchemeId,
                SchemeName = schemeDTO.SchemeName,
                Status = true,
                InsurancePlanId = schemeDTO.PlanId,
                Policies = new List<Policy>(),


            };
            return Scheme;

        }
        private InsuranceSchemeShowDTO ConvertToSchemeDTO(InsuranceScheme scheme)
        {
            var SchemeDTO = new InsuranceSchemeShowDTO()
            {
                SchemeId = scheme.SchemeId,
                SchemeName = scheme.SchemeName,
                PlanId = scheme.InsurancePlanId, 
                PoliciesCount = scheme.Plans != null ? scheme.Policies.Count() : 0,
                status= scheme.Status,
            };
            return SchemeDTO;
        }

        [HttpPost("AddSchemeWithDetails")]
        public async Task<IActionResult> AddSchemeWithDetails([FromBody] InsuranceDetailsDTO viewModel)
        {
            if (viewModel == null)
                return BadRequest("Invalid data");

            var scheme = ConvertToScheme(viewModel);
            var schemeDetails = ConvertToSchemeDetails(viewModel) ;

            await _insuranceSchemesService.AddSchemeWithDetails(scheme, schemeDetails);

            return Ok(scheme.SchemeId);
        }
        private SchemeDetails ConvertToSchemeDetails(InsuranceDetailsDTO schemeDTO)
        {
            var Scheme = new SchemeDetails()
            {
                //     SchemeId = schemeDTO.SchemeId,
                DetailId=0,
                SchemeImage = schemeDTO.SchemeImage,
                MaxAge = schemeDTO.MaxAge,
                MinAge = schemeDTO.MinAge,
                MinAmount= schemeDTO.MinAmount,
                MaxAmount= schemeDTO.MaxAmount,
                MinTerm=schemeDTO.MinTerm,
                MaxTerm=schemeDTO.MaxTerm,
                FirstPremiumCommissionPercent=schemeDTO.FirstPremiumCommissionPercent,
                EMICommissionPercent=schemeDTO.EMICommissionPercent,
                ProfitPercent=schemeDTO.ProfitPercent,
                Description=schemeDTO.Description,

                
                Status = true,


            };
            return Scheme;

        }
    }
}
