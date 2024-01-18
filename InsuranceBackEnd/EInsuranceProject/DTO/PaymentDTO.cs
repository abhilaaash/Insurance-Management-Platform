using EInsuranceProject.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsuranceProject.DTO
{
    public class PaymentDTO
    { 
      //  public int PaymentId { get; set; }
        [Required]
        public string PaymentType { get; set; }
        [Required]
        public double Amount { get; set; }
     //   [Required]
     //   public DateTime PaymentDate { get; set; }
        [Required]
        public double Tax { get; set; }
        public double TotalPayment { get; set; }
        public string? CardNumber { get; set; }

        public string? cvv { get; set; }
        public bool Status { get; set; }
        public int PolicyNo { get; set; }
        public int CustomerId { get; set; }
       
    }
}
