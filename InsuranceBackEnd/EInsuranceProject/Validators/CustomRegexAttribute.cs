namespace EInsuranceProject.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class CustomRegexAttribute : ValidationAttribute
    {
        public CustomRegexAttribute(string pattern) : base("Invalid characters.")
        {
            Pattern = pattern;
        }

        public string Pattern { get; private set; }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            if (value is string str)
            {
                return System.Text.RegularExpressions.Regex.IsMatch(str, Pattern);
            }
            return false;
        }
    }
}
