using System.ComponentModel.DataAnnotations;

namespace EInsuranceProject.Validators
{
    public class CustomDateOfComplaintAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true; // Null is considered valid. You can change this logic if needed.
            }

            if (value is DateTime complaintDate)
            {
                // Define your minimum allowed date for complaints here (e.g., not in the future)
                return complaintDate <= DateTime.Now;
            }

            // If the value is not a DateTime, it's considered invalid.
            return false;
        }
    }
}
