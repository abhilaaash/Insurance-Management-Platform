using EInsuranceProject.Validators;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsuranceProject.Model
{
    public class Policy
    {
        [Key]
        public int PolicyId { get; set; }

        [Required(ErrorMessage = "Issue Date is required")]
        [CustomDateOfComplaint(ErrorMessage = "Datetime must be correct.")]
        public DateTime IssueDate { get; set; }

        [Required(ErrorMessage = "Maturity Date is required")]
        [CustomDateOfComplaint(ErrorMessage = "Datetime must be correct.")]
        public DateTime MaturityDate { get; set; }

        [Required(ErrorMessage = "Premium Mode is required")]
        [EnumDataType(typeof(Mode), ErrorMessage = "Invalid Premium Mode")]
        public Mode PremiumMode { get; set; }


        [Required(ErrorMessage = "Total Premium No is required")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]
        public int TotalPremiumNo { get; set; }

        [Required(ErrorMessage = "Premium is required")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]
        public double Premium { get; set; }

        [Required(ErrorMessage = "Sum Assured is required")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]
        public double SumAssured { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public bool Status { get; set; }

        public List<Nominie> Nominees { get; set; }

        public Customer? Customer { get; set; }
        [ForeignKey("Customer")]
        [Required(ErrorMessage = "CustomerId is required")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]
        public int CustomerId { get; set; }

        public Agent? Agent { get; set; }
        [ForeignKey("Agent")]
        public int? AgentId { get; set; }

        public List<Payment>? Payments { get; set; }

        public InsuranceScheme? InsuranceScheme { get; set; }
        [ForeignKey("InsuranceScheme")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]
        public int InsuranceSchemeId { get; set; }

        public List<Claim>? Claims { get; set; }

    }
}
