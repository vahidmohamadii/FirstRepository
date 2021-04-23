using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstRespository.Api.Dtos.Person;
using FluentValidation;

namespace FirstRespository.Api.Validators
{
    public sealed class RemovePersonValidator : AbstractValidator<RemovePersonDto>
    {
        public RemovePersonValidator()
        {
            RuleFor(model => model.Id)
                .NotNull().WithMessage("Required")
                .NotEmpty().WithMessage("Required");
        }
    }
}
