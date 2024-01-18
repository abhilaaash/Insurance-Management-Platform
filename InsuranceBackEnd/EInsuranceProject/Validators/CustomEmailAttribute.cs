using System.Text.RegularExpressions;

namespace EInsuranceProject.Validators
{
    public class CustomEmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true; // Null is considered valid. You can change this logic if needed.
            }

            var emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$";
            var regex = new Regex(emailPattern);

            return regex.IsMatch(value.ToString());
        }
    }

}
