using BankingAPI.Kernel.Wrapper;

namespace EInsuranceProject.Wrapper
{
    public class Parameter:PageParameters
    {
        public string? FirstName { get; set; }
        public int ? Id { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

    }
}
