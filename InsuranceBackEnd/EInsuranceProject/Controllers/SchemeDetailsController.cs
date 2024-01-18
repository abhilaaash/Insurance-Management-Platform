using EInsuranceProject.DTO;
using EInsuranceProject.Model;
using EInsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EInsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchemeDetailsController : ControllerBase
    {
        private ISchemeDetailsService _schemeDetailsService;
        public SchemeDetailsController(ISchemeDetailsService schemeDetailsService)
        {
            _schemeDetailsService = schemeDetailsService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetSchemeDtails()
        {
            string[] innerTable = { };
            var schemeDetails = await _schemeDetailsService.GetAll(innerTable);
            var SchemeDetailDTOS = new List<SchemeDetailShowDTO>();
            foreach (var schemeDetail in schemeDetails)
            {
                SchemeDetailDTOS.Add(ConvertToSchemeDetailDTO(schemeDetail));
            }
            return Ok(SchemeDetailDTOS);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByID([FromQuery] int Id)
        {

            var schemeDetails = await _schemeDetailsService.GetById(Id);
            var schemeDetailDTO = ConvertToSchemeDetailDTO(schemeDetails);
            return Ok(schemeDetailDTO);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddSchemeDetail([FromBody] SchemeDetailDTO schemeDetailDTO)
        {
            var schemeDetail = ConvertToSchemeDetail(schemeDetailDTO);
            await _schemeDetailsService.AddSchemeDetail(schemeDetail);
            return Ok(schemeDetail);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] SchemeDetailDTO schemeDetailDTO)
        {
            var newSchemeDetail = ConvertToSchemeDetail(schemeDetailDTO);
            await _schemeDetailsService.UpdateSchemeDetail(newSchemeDetail);
            return Ok(newSchemeDetail.DetailId);
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _schemeDetailsService.DeleteSchemeDetail(id);

            return Ok("RemovedSuccessfully");


        }
        private SchemeDetails ConvertToSchemeDetail(SchemeDetailDTO schemeDetailDTO)
        {
            var schemDetail = new SchemeDetails()
            {
               
                DetailId= schemeDetailDTO.DetailId,
                Description= schemeDetailDTO.Description,
                MaxAmount= schemeDetailDTO.MaxAmount,
                MinAmount= schemeDetailDTO.MinAmount,
                SchemeImage= schemeDetailDTO.SchemeImage,
                Status= true,
                MinAge= schemeDetailDTO.MinAge,
                MaxAge= schemeDetailDTO.MaxAge,
                MinTerm=schemeDetailDTO.MinTerm,
                MaxTerm=schemeDetailDTO.MaxTerm,
                ProfitPercent=schemeDetailDTO.ProfitPercent,
                EMICommissionPercent=schemeDetailDTO.EMICommissionPercent,
                FirstPremiumCommissionPercent= schemeDetailDTO.FirstPremiumCommissionPercent
                

            };
            return schemDetail;

        }
        private SchemeDetailShowDTO ConvertToSchemeDetailDTO(SchemeDetails schemeDetails)
        {
            var schemDetailDTO = new SchemeDetailShowDTO()
            {

                DetailId = schemeDetails.DetailId,
                Description = schemeDetails.Description,
                MaxAmount = schemeDetails.MaxAmount,
                MinAmount = schemeDetails.MinAmount,
                SchemeImage = schemeDetails.SchemeImage,
                MaxAge= schemeDetails.MaxAge,
                MinAge= schemeDetails.MinAge,
                MinTerm=schemeDetails.MinTerm,
                MaxTerm=schemeDetails.MaxTerm,
                ProfitPercent=schemeDetails.ProfitPercent,
                FirstPremiumCommissionPercent=schemeDetails.FirstPremiumCommissionPercent,
                EMICommissionPercent=schemeDetails.EMICommissionPercent
            };
            return schemDetailDTO;

        }
    }
}
