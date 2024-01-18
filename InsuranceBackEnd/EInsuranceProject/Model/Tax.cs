namespace EInsuranceProject.Model
{
    public class Tax
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Total Premium No is required")]
        [CustomNonNegativeNumber(ErrorMessage = "Value must be a non-negative number.")]

        public double TaxPercent { get; set; }
    }
}
