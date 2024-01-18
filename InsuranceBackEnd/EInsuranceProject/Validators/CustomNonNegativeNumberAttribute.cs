namespace EInsuranceProject.Validators
{
    public class CustomNonNegativeNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true; // Null is considered valid. You can change this logic if needed.
            }

            if (value is int intValue)
            {
                return intValue >= 0;
            }

            if (value is double doubleValue)
            {
                return doubleValue >= 0;
            }

            // If the value is not an int or double, it's considered invalid.
            return false;
        }
    }
}
