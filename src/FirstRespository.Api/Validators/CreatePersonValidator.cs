using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstRespository.Api.Dtos.Person;
using FluentValidation;

namespace FirstRespository.Api.Validators
{
    public sealed class CreatePersonValidator : AbstractValidator<CreatePersonDto>
    {
        public CreatePersonValidator()
        {
            RuleFor(model => model.FirstName)
                .NotNull().WithMessage("Required")
                .NotEmpty().WithMessage("Required");

            RuleFor(model => model.LastName)
                .NotNull().WithMessage("Required")
                .NotEmpty().WithMessage("Required");

            RuleFor(model => model.Age)
                .NotNull().WithMessage("Required")
                .NotEmpty().WithMessage("Required")
                .GreaterThanOrEqualTo(18).When(model=>model.Age != default, ApplyConditionTo.CurrentValidator).WithMessage("Must greater than 18")
                .LessThanOrEqualTo(60).When(model => model.Age != default, ApplyConditionTo.CurrentValidator).WithMessage("Must lower than 60");
        }
    }
}
