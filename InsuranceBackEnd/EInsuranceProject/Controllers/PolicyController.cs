using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.DTO;
using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using EInsuranceProject.Services;
using FinalProjectInsurance.DTO;
using FinalProjectInsurance.Model;
using FinalProjectInsurance.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EInsuranceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private IPolicyService _policyService;
        private IPaymentService _paymentService;
        private INominieService _nominieService;
        private ICommissionService _commissionService;
        private ISchemeDetailsService _schemeDetailsService;
        public PolicyController(IPolicyService policyService,IPaymentService paymentService,INominieService nominieService,ICommissionService commissionService,ISchemeDetailsService schemeDetailsService)
        {
            _nominieService = nominieService;
            _paymentService= paymentService;
            _policyService = policyService;
            _commissionService = commissionService;
            _schemeDetailsService = schemeDetailsService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetPolicies()
        {
            string[] innerTable = { "Payments", "Claims" };
            var policies = await _policyService.GetAll(innerTable);
            var policyDTOS = new List<PolicyShowDTO>();
            foreach (var policy in policies)
            {
                policyDTOS.Add(ConvertToPolicyDTO(policy));
            }
            return Ok(policyDTOS);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByID([FromQuery] int Id)
        {

            var policy = await _policyService.GetById(Id);
            var policyDTO = ConvertToPolicyDTO(policy);
            return Ok(policyDTO);
        }
        //[HttpPost("Add")]
        //public async Task<IActionResult> AddPolicy([FromBody] PolicyDTO policyDTO)
        //{
        //    var policy = ConvertToPolicy(policyDTO);
        //    await _policyService.AddPolicy(policy);
        //    return Ok(policy.PolicyId);
        //}
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] PolicyUpdateDTO policyDTO)
        {
            var newPolicy = ConvertToPolicyUpdate(policyDTO);
            await _policyService.UpdatePolicy(newPolicy);
            return Ok(newPolicy.PolicyId);
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _policyService.DeletePolicy(id);

            return Ok(id);


        }

        //----------------------------------------------------------------------------

        [HttpGet("GetByCustomerId/{customerId}")]
        public async Task<IActionResult> GetPoliciesByCustomerId(int customerId)
        {
            var policies = await _policyService.GetPoliciesByCustomerId(customerId);
            return Ok(policies);
        }
        [HttpGet("GetByAgentId/{agentId}")]
        public async Task<IActionResult> GetPoliciesByAgentIdAsync(int agentId)
        {
            var policies = await _policyService.GetPoliciesByAgentIdAsync(agentId);
            return Ok(policies);
        }

        private Policy ConvertToPolicy(PolicyDTO policyDTO)
        {
            var policy = new Policy()
            {
               // PolicyId = policyDTO.PolicyNo,
                Premium = policyDTO.Premium,
            //    PremiumMode = Mode.Year,
                IssueDate = DateTime.Now,
                MaturityDate =policyDTO.MaturityDate,
                SumAssured = policyDTO.SumAssured,
                PremiumMode=policyDTO.PremiumMode,
                TotalPremiumNo=policyDTO.TotalPremiumNo,
                
           //     Nominee=policyDTO.Nominee,
                Nominees=new List<Nominie>(),
                CustomerId = policyDTO.CustomerId,
                AgentId = policyDTO.AgentId!=0?policyDTO.AgentId:null,
                InsuranceSchemeId = policyDTO.SchemeId,
                Status = true,
           //     Customers = new List<Customer>(),
            //    Payments=new List<Payment>(),
                Claims=new List<Claim>(),
            };
            return policy;

        }
        private PolicyShowDTO ConvertToPolicyDTO(Policy Policy)
        {
            var policyDTO = new PolicyShowDTO()
            {
                PolicyNo = Policy.PolicyId,
                Premium = Policy.Premium,
                SumAssured = Policy.SumAssured,
                IssueDate = Policy.IssueDate,
                MaturityDate = Policy.MaturityDate,
                PremiumMode = Policy.PremiumMode,
                TotalPremiumNo = Policy.TotalPremiumNo,
                //    PremiumMode=Policy.PremiumMode,
                CustomerId = Policy.CustomerId,
                AgentId = Policy.AgentId,
                SchemeId = Policy.InsuranceSchemeId,
                Status = true,
                NomineeCount = Policy.Nominees != null ? Policy.Nominees.Count() : 0,
               PaymentCount = Policy.Payments != null ? Policy.Payments.Count() : 0,
               ClaimsCount = Policy.Claims != null ? Policy.Claims.Count() : 0

            };
            return policyDTO;
        }
        private Policy ConvertToPolicyUpdate(PolicyUpdateDTO policyDTO)
        {
            var policy = new Policy()
            {
                PolicyId = policyDTO.PolicyNo,
                Premium = policyDTO.Premium,
                //    PremiumMode = Mode.Year,
              //  IssueDate = DateTime.Now,
                MaturityDate = DateTime.Now,
                SumAssured = policyDTO.SumAssured,
                //     Nominee=policyDTO.Nominee,
                Nominees = new List<Nominie>(),
                CustomerId = policyDTO.CustomerId,
                AgentId = policyDTO.AgentId != 0 ? policyDTO.AgentId : null,
                InsuranceSchemeId = policyDTO.SchemeId,
                Status = true,
                //     Customers = new List<Customer>(),
                //    Payments=new List<Payment>(),
                Claims = new List<Claim>(),
            };
            return policy;

        }

        [HttpGet("get")]

        public async Task<IActionResult> GetPolicy([FromQuery] PageParameters pageParameter)

        {

            string[] innerTable = { };

            var customers = await _policyService.GetAsync(pageParameter);

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





        //-----------------------------------------------------------------






        [HttpPost("add")]
        public async Task<IActionResult> AddPolicys([FromBody] PolicyDTO policyDTO)
        {

            var policy = await _policyService.AddPolicyReturn(ConvertToPolicy(policyDTO));
            await _paymentService.AddPayment(ConvertToPayment(policyDTO.PaymentDTO, policy.PolicyId));
            //--just chnged   
                if (policyDTO.AgentId > 0)
                {
                    Commission commission = await ConvertToCommission( policy.PolicyId, policy.InsuranceSchemeId, policyDTO.PaymentDTO.Amount, policyDTO.AgentId);
                    await _commissionService.AddCommission(commission);
                }
            //var Payment = ConvertToPayment(policyDTO.PaymentDTO, policy.PolicyNo);
            //policy.Payments.Add(Payment);
            if (policyDTO.NomineeDTO != null)
            {
                await _nominieService.AddNominie(ConvertToNominee(policyDTO.NomineeDTO, policy.PolicyId));
            }
            return Ok(policy.PolicyId);
        }



        private Payment ConvertToPayment(PaymentDTO paymentDTO, int policyNo)
        {
            var payment = new Payment()
            {
                PaymentDate = DateTime.Now,
                PaymentType = paymentDTO.PaymentType,
                Amount = paymentDTO.Amount,
                Tax = paymentDTO.Tax,
                TotalPayment = paymentDTO.TotalPayment,
                PolicyNo = policyNo,
                CustomerId=paymentDTO.CustomerId,
                Status = paymentDTO.Status,
                CardNumber = paymentDTO.CardNumber,
                cvv=paymentDTO.cvv,
                
               

            };
            return payment;
        }
        private Nominie ConvertToNominee(NomineeDTO nomineeDTO, int policyNo)
        {
            return new Nominie()
            {
                NominieName = nomineeDTO.NomineeName,
                NominieRelation = nomineeDTO.Relation,
                PolicyNo = policyNo,
                Status=true
                
            };
        }
        //----------------------------------------------just chngeddd
        private async Task<Commission> ConvertToCommission(int policyId,int schemeId,double premium,int? agentid)
        {

            int amount =await CalculateFirstPremiumCommissionAsync(premium, schemeId);
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

        private async Task<int> CalculateFirstPremiumCommissionAsync(double premiumAmount, int schemeId)
        {
            SchemeDetails schemeDetails = await _schemeDetailsService.GetSchemeDetailsByIdAsync(schemeId);
            if (schemeDetails != null)
            {
                double firstPremiumCommissionPercent = schemeDetails.FirstPremiumCommissionPercent;

                // Calculate first premium commission
                double firstPremiumCommission = (premiumAmount * firstPremiumCommissionPercent) / 100;

                return (int)firstPremiumCommission;
            }

            throw new Exception("SchemeDetails not found for detailId: " + schemeId);
        }
    }
}
