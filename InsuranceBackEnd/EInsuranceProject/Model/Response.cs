using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectInsurance.Model
{
    public class Response
    {
        public int ResponseId { get; set; }
        public bool Status { get; set; }

        public string ?ResponseMessage { get; set; }

        public Complaint ? Complaint { get; set; }

        [ForeignKey("Complaint")]
        public int ComplaintId { get; set; }

    }
}
