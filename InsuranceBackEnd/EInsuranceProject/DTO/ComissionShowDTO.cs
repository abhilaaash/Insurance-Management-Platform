namespace EInsuranceProject.DTO
{
    public class ComissionShowDTO
    {
        public int CommissionId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public int PolicyId { get; set; }
        public int AgentId { get; set; }

        public bool status { get; set; }
       
    }
}
