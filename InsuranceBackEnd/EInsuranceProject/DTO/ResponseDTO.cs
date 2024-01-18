using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectInsurance.DTO
{
    public class ResponseDTO
    {
   //     public int ResponseId { get; set; }
        public bool Status { get; set; }

        public string? ResponseMessage { get; set; }


       // public Complaint? Complaint { get; set; }

       
        public int ComplaintId { get; set; }
    }
}
