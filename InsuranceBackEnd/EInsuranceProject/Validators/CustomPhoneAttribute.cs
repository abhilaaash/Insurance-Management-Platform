using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EInsuranceProject.Validators
{
    public class CustomPhoneAttribute :ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true; // Null is considered valid. You can change this logic if needed.
            }

            var phonePattern = "^[0-9]{10}$";
            var regex = new Regex(phonePattern);

            return regex.IsMatch(value.ToString());
        }
    }
}
