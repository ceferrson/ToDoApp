using FluentValidation;
using ToDoAppNTier.Dtos.Dtos;

namespace ToDoAppNTier.Business.ValidationRules
{
    public class WorkDtoValidator : AbstractValidator<WorkDto>
    {
        public WorkDtoValidator()
        {
            RuleFor(wd => wd.Title).MaximumLength(100).WithMessage("Title must contain 100 characters maximum").NotEmpty();
        }
    }
}
