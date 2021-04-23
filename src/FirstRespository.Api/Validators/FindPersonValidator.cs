using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstRespository.Api.Dtos.Person;
using FluentValidation;

namespace FirstRespository.Api.Validators
{
    public sealed class FindPersonValidator : AbstractValidator<FindPersonDto>
    {
        public FindPersonValidator()
        {
            RuleFor(model => model.Id)
                .NotNull().WithMessage("Required")
                .NotEmpty().WithMessage("Required");
        }
    }
}
