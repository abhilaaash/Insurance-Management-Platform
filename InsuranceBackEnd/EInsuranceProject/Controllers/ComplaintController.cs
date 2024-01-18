using EInsuranceProject.DTO;
using EInsuranceProject.Model;
using EInsuranceProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EInsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private IComplaintService _complaintService;
        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetCompalints()
        {
            string[] innerTable = { };
            var complaints = await _complaintService.GetAll(innerTable);
            var ComplaintDTOS = new List<ComplaintShowDTO>();
            foreach (var complaint in complaints)
            {
                ComplaintDTOS.Add(ConvertToComplaintDTO(complaint));
            }
            return Ok(ComplaintDTOS);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByID([FromQuery] int customerId)
        {

            var Complaints = await _complaintService.GetById(customerId);
            var ComplaintDTO = new List<ComplaintShowDTO>();
            foreach (var complaint in Complaints)
            {
                ComplaintDTO.Add(ConvertToComplaintDTO(complaint));
            }
            return Ok(ComplaintDTO);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddCompalint([FromBody] ComplaintDTO complaintDTO)
        {
            var Complaint = ConvertToComplaint(complaintDTO);
            await _complaintService.AddComplaint(Complaint);
            return Ok(Complaint);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ComplaintUpdateDTO complaintDTO)
        {
            var newComplaint = ConvertToComplaintUpdate(complaintDTO);
            await _complaintService.UpdateComplaint(newComplaint);
            return Ok(newComplaint);
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _complaintService.DeleteComplaint(id);

            return Ok(id);


        }
        private Complaint ConvertToComplaint(ComplaintDTO ComplaintDTO)
        {
            var Complaint = new Complaint()
            {
            //    ComplaintId = ComplaintDTO.ComplaintId,
                ComplaintName = ComplaintDTO.ComplaintName,
                ComplaintMessage = ComplaintDTO.ComplaintMessage,
                DateOfComplaint = DateTime.Now,
                Status = true,
                CustomerId = ComplaintDTO.CustomerId,
            };
            return Complaint;

        }
        private ComplaintShowDTO ConvertToComplaintDTO(Complaint complaint)
        {
            var ComplaintDTO = new ComplaintShowDTO()
            {

                ComplaintId = complaint.ComplaintId,
                ComplaintName = complaint.ComplaintName,
                ComplaintMessage = complaint.ComplaintMessage,
                DateOfComplaint = complaint.DateOfComplaint,
                Reply=complaint.Reply,
              //  Status = complaint.Status,
                CustomerId = complaint.CustomerId
            };
            return ComplaintDTO;
        }
        private Complaint ConvertToComplaintUpdate(ComplaintUpdateDTO ComplaintDTO)
        {
            var Complaint = new Complaint()
            {
                  ComplaintMessage=ComplaintDTO.ComplaintMessage ,
                  ComplaintId = ComplaintDTO.ComplaintId,
                  Reply= ComplaintDTO.Reply,
                  DateOfComplaint=ComplaintDTO.DateOfComplaint,
                  
              Status=true,
              CustomerId= ComplaintDTO.CustomerId,
              ComplaintName = ComplaintDTO.ComplaintName
              
            };
            return Complaint;

        }
    }
}
