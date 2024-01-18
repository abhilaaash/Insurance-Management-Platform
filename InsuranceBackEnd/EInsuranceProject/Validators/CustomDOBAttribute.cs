using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.Validators
{
    public class CustomDOBAttribute :  ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true; // Null is considered valid. You can change this logic if needed.
            }

            if (value is DateTime dob)
            {
                // Calculate the minimum allowed date (e.g., 18 years ago)
                var minDOB = DateTime.Now.AddYears(-18);
                return dob <= minDOB;
            }

            // If the value is not a DateTime, it's considered invalid.
            return false;
        }
    }
}
