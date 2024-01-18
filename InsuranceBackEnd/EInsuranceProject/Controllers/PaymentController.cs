using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.DTO;
using EInsuranceProject.Model;
using EInsuranceProject.Services;
using FinalProjectInsurance.Model;
using FinalProjectInsurance.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EInsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IPaymentService _paymentService;
        private INominieService _nominieService;
        private ICommissionService _commissionService;
        private ISchemeDetailsService _schemeDetailsService;
        private IPolicyService _policyService;  
        public PaymentController(IPaymentService paymentService,INominieService nominieService,ICommissionService commissionService,ISchemeDetailsService schemeDetailsService,IPolicyService policyService)

        {
            _paymentService = paymentService;
            _nominieService= nominieService;
            _commissionService= commissionService;
            _schemeDetailsService= schemeDetailsService;
            _policyService= policyService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetPayments()
        {
            string[] innerTable = {  };
            var Payments = await _paymentService.GetAll(innerTable);
            var PaymentDTOS = new List<PaymentShowDTO>();
            foreach (var payment in Payments)
            {
                PaymentDTOS.Add(ConvertToPaymentDTO(payment));
            }
            return Ok(PaymentDTOS);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByID([FromQuery] int Id)
        {

            var Payment = await _paymentService.GetById(Id);
            var PaymentDTO = ConvertToPaymentDTO(Payment);
            return Ok(PaymentDTO);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddPayment([FromBody] PaymentDTO paymentDTO)
        {
            var Payment = ConvertToPayment(paymentDTO);
            Policy policy = await _policyService.GetById(paymentDTO.PolicyNo);
            if (policy.AgentId > 0&&policy.AgentId!=null)
            {
                Commission commission = await ConvertToCommission(paymentDTO.PolicyNo,paymentDTO.Amount,policy.AgentId,policy.InsuranceSchemeId);
                await _commissionService.AddCommission(commission);
            }
            await _paymentService.AddPayment(Payment);
            return Ok(Payment);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] PaymentDTO paymentDTO)
        {
            var newPayment = ConvertToPayment(paymentDTO);
            await _paymentService.UpdatePayment(newPayment);
            return Ok("Updated Successfully");
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _paymentService.DeletePayment(id);

            return Ok("Removed Successfully");


        }
        private Payment ConvertToPayment(PaymentDTO paymentDTO)
        {
            var Payment = new Payment()
            {
             //   PaymentId = paymentDTO.PaymentId,
                PaymentDate = DateTime.Now,
                PaymentType = paymentDTO.PaymentType,
                Amount = paymentDTO.Amount,
                Tax = paymentDTO.Tax,
                TotalPayment = paymentDTO.TotalPayment,
                CustomerId = paymentDTO.CustomerId,
                PolicyNo = paymentDTO.PolicyNo,
                Status = false,
                CardNumber = paymentDTO.CardNumber,
                cvv = paymentDTO.cvv,
                


            };
            return Payment;

        }
        private PaymentShowDTO ConvertToPaymentDTO(Payment payment)
        {
            var paymentDTO = new PaymentShowDTO()
            {
                PaymentId=payment.PaymentId,
                PaymentDate=payment.PaymentDate,
                PaymentType=payment.PaymentType,
                Amount=payment.Amount,
                Tax=payment.Tax,
                TotalPayment=payment.TotalPayment,
                CustomerId=payment.CustomerId,
                PolicyNo=payment.PolicyNo,
                Status=payment.Status,
               CardNumber=payment.CardNumber
            };
            return paymentDTO;
        }

        [HttpGet("GetByCustomerId/{customerId}")]
        public async Task<IActionResult> GetPaymentsByCustomerIdAsync(int customerId)
        {
            var payments = await _paymentService.GetPaymentsByCustomerIdAsync(customerId);
            return Ok(payments);
        }

        [HttpGet("get")]

        public async Task<IActionResult> GetPayment([FromQuery] PageParameters pageParameter)

        {

            string[] innerTable = { };

            var customers = await _paymentService.GetAsync(pageParameter);

            var metadata = new

            {

                customers.TotalCount,

                customers.PageSize,

                customers.CurrentPage,

                customers.TotalPages,

                customers.HasNext,

                customers.HasPrevious,

            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));



            return Ok(customers);

        }


        private async Task<Commission> ConvertToCommission(int policyId, double premium, int? agentid,int schemeId)
        {
          
           
       

            int amount = await CalculateFirstPremiumCommissionAsync(premium, schemeId);
            var commission = new Commission()
            {
                // CommissionId=commissionDTO.CommissionId,
                Amount = amount,
                Date = DateTime.Now,
                AgentId = (int)agentid,
                PolicyId = policyId,
                status = false

            };
            return commission;

        }

        private async Task<int> CalculateFirstPremiumCommissionAsync(double premiumAmount,int schemeId)
        {
            
            
            SchemeDetails schemeDetails = await _schemeDetailsService.GetSchemeDetailsByIdAsync(schemeId);
            if (schemeDetails != null)
            {
                double emiPremiumCommissionPercent = schemeDetails.EMICommissionPercent;

                // Calculate first premium commission
                double eirstPremiumCommission = (premiumAmount * emiPremiumCommissionPercent) / 100;

                return (int)eirstPremiumCommission;
            }

            throw new Exception("SchemeDetails not found for detailId: " + schemeId);
        }

    }
}
