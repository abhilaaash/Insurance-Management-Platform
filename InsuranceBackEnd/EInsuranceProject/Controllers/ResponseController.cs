using EInsuranceProject.DTO;
using EInsuranceProject.Model;
using FinalProjectInsurance.DTO;
using FinalProjectInsurance.Model;
using FinalProjectInsurance.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectInsurance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private IResponseService _responseService;
        public ResponseController(IResponseService responseService)
        {
            _responseService = responseService;
        }
        [HttpGet("GetAllResponses")]
        public async Task<IActionResult> GetResponses()
        {
            string[] innerTable = { };
            var responses = await _responseService.GetAll(innerTable);
            var responseDTOS = new List<ResponseShowDTO>();
            foreach (var response in responses)
            {
                responseDTOS.Add(ConvertToResponseDTO(response));
            }
            return Ok(responseDTOS);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByID([FromQuery] int Id)
        {

            var response = await _responseService.GetById(Id);
            var responseDTO = ConvertToResponseDTO(response);
            return Ok(responseDTO);
        }
        [HttpPost("AddResponse")]
        public async Task<IActionResult> AddResponse([FromBody] ResponseDTO responseDTO)
        {
            var response = ConvertToResponse(responseDTO);
            await _responseService.AddResponse(response);
            return Ok(response);
        }
        [HttpPut("UpdateResponse")]
        public async Task<IActionResult> Update([FromBody] ResponseDTO responseDTO)
        {
            var newResponse = ConvertToResponse(responseDTO);
            await _responseService.UpdateResponse(newResponse);
            return Ok("Updated Successfully");
        }
        [HttpDelete("DeleteResponse/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _responseService.DeleteResponse(id);

            return Ok("RemovedSuccessfully");


        }
        private Response ConvertToResponse(ResponseDTO responseDTO)
        {
            var response = new Response()
            {
              //  ResponseId = responseDTO.ResponseId,
                ResponseMessage = responseDTO.ResponseMessage,
                Status = true,
                 ComplaintId = responseDTO.ComplaintId
                

            };
            return response;

        }
        private ResponseShowDTO ConvertToResponseDTO(Response response)
        {
            var responseDTO = new ResponseShowDTO()
            {
                ResponseId = response.ResponseId,
                ResponseMessage = response.ResponseMessage,
                ComplaintId= response.ComplaintId
               
            };
            return responseDTO;
        }
    }
}
