using FluentValidation;
using ToDoAppNTier.Dtos.Dtos;

namespace ToDoAppNTier.Business.ValidationRules
{
    public class WorkCreateDtoValidator : AbstractValidator<WorkCreateDto>
    {
        public WorkCreateDtoValidator()
        {
            //SO we define that our title can be 20 characters in maximum and in addition to if its condition is completed so title can't be empty.
            RuleFor(wc => wc.Title).MaximumLength(20).WithMessage("Title must contains only 20 characters").NotEmpty();

            //bu sekilde daxiline funksiyada vere bilerik
            RuleFor(wc => wc.Title).Must(CantTakeNameMath).WithMessage("Task name can't take Math or math");
        }

        private bool CantTakeNameMath(string arg)
        {
            return arg != "Math" && arg != "math";
        }
    }
}
