using FluentValidation;

namespace MinimalWebAPI.ToDo
{
    public class ToDoValidator : AbstractValidator<ToDo>
    {
        public ToDoValidator()
        {
            RuleFor(t=>t.Name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100)
                .WithMessage("5 chars minimum and 100 max");
        }
    }
}
