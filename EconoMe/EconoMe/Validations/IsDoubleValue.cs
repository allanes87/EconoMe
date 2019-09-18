using System;
namespace EconoMe.Validations
{
    public class IsDoubleValue<T> : IValidationRule<T>
    {
        public IsDoubleValue()
        {
            ValidationMessage = AppResource.Validations_IsDoubleValueMessage;
        }

        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            double doubleValue;
            var result = double.TryParse(value.ToString(), out doubleValue);

            return result;
        }
    }
}
