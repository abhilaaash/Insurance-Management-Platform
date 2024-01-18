namespace EInsuranceProject.DTO
{
    public class InsuranceSchemeDetailShowDTO
    {
        public int PlanId { get; set; }
        public string SchemeName { get; set; }
        public string SchemeImage { get; set; }
        public string Description { get; set; }
        public Int32 MinAmount { get; set; }
        public Int32 MaxAmount { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }
}
