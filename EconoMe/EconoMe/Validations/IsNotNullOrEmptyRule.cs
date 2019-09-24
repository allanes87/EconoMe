using EconoMe.Helpers;

namespace EconoMe.Validations
{
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public IsNotNullOrEmptyRule()
        {
            ValidationMessage = string.Empty;
        }

        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value.ToString();

            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
