using EInsuranceProject.DTO;
using EInsuranceProject.Model;
using EInsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EInsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NominieController : ControllerBase
    {

        private INominieService _nominieService;

        public NominieController(INominieService nominieService)
        {
            _nominieService = nominieService;

        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetNominie()
        {
            string[] innerTable = { };
            var nominies = await _nominieService.GetAll(innerTable);
            var nominieDTOS = new List<NominieShowDTO>();
            foreach (var nominie in nominies)
            {
                nominieDTOS.Add(ConvertToNominieDTO(nominie));
            }
            return Ok(nominieDTOS);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByID([FromQuery] int Id)
        {

            var nominie = await _nominieService.GetById(Id);
            var nominieDTO = ConvertToNominieDTO(nominie);
            return Ok(nominieDTO);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddNominie([FromBody] NomineeDTO nominieDTO)
        {
            var nominie = ConvertToNominee(nominieDTO);
            await _nominieService.AddNominie(nominie);
            return Ok(nominie);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateNominie([FromBody] NominieDTO nominieDTO)
        {
            var newNominie = ConvertToNominie(nominieDTO);
            await _nominieService.UpdateNominie(newNominie);
            return Ok("Updated Successfully");
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _nominieService.DeleteNominie(id);

            return Ok("RemovedSuccessfully");


        }

        private Nominie ConvertToNominie(NominieDTO nominieDTO)
        {
            var nominie = new Nominie()
            {
                NominieId = nominieDTO.NominieId,
                NominieName = nominieDTO.NominieName,
                NominieRelation = nominieDTO.NominieRelation,
                PolicyNo = nominieDTO.PolicyNo,
                Status = true,

            };
            return nominie;

        }
        private Nominie ConvertToNominee(NomineeDTO nominieDTO)
        {
            var nominie = new Nominie()
            {
                NominieName = nominieDTO.NomineeName,
                NominieRelation = nominieDTO.Relation,
                Status = true,

            };
            return nominie;

        }
        private NominieShowDTO ConvertToNominieDTO(Nominie nominie)
        {
            var nominieDTO = new NominieShowDTO()
            {
                NominieId = nominie.NominieId,
                NomineeName = nominie.NominieName,
                Relation = nominie.NominieRelation,
                PolicyNo = nominie.PolicyNo

                //   status = nominie.Status,

            };
            return nominieDTO;
        }

        [HttpGet("GetByCustomerId/{customerId}")]
        public async Task<IActionResult> GetNominieeByCustomerIdAsync(int customerId)
        {
            var nominies = await _nominieService.GetNominieeByCustomerIdAsync(customerId);
            return Ok(nominies);
        }
    }
}

