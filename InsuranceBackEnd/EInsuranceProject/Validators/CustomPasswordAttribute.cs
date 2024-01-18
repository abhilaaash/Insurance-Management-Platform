using System.Text.RegularExpressions;

namespace EInsuranceProject.Validators
{
    public class CustomPasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true; // Null is considered valid. You can change this logic if needed.
            }

            var passwordPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";
            var regex = new Regex(passwordPattern);

            return regex.IsMatch(value.ToString());
        }
    }
}
