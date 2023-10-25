using ToDoAppNTIer.Common.Response;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ToDoAppNTier.Business.Extensions
{
    public static class ValidationResultExtension
    {
        public static List<CustomValidationErrors> ConvertToCustomValidationError(this ValidationResult validationResult)
        {
            List<CustomValidationErrors> errors = new();
            foreach (var error in validationResult.Errors)
            {
                errors.Add(new()
                {
                    ErrorMesage = error.ErrorMessage,
                    PropertyName = error.PropertyName
                });
            }
            return errors;
        }
    }
}
