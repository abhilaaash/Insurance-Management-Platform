using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsuranceProject.Model
{
    public class Payment
    {
        [Key]
        public int PaymentId { get;set; }
        [Required]
        public string PaymentType { get;set; }
        [Required]
        public double Amount { get;set; }
        [Required]
        public DateTime PaymentDate { get;set; }
        [Required]
        public double Tax { get;set; }
        public double TotalPayment { get;set; }
        public bool Status { get; set; }

        public string? CardNumber { get; set; }

        public string? cvv { get; set; }

        // public List<Policy> Policies { get;set; }
        
        [ForeignKey("PolicyNo")]
        public Policy policy { get; set; }
       
        public int PolicyNo { get; set; }
        public Customer Customer { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

    }
}
