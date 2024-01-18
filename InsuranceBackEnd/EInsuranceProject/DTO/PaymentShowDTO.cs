using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.DTO
{
    public class PaymentShowDTO
    {
        public int PaymentId { get; set; }
        public string PaymentType { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string CardNumber { get; set; }

       
        public double Tax { get; set; }
        public double TotalPayment { get; set; }
        public bool Status { get; set; }
        public int PolicyNo { get; set; }
        public int CustomerId { get; set; }
    }
}
