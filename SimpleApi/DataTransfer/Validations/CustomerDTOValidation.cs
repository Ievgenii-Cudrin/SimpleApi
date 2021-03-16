using FluentValidation;
using SimpleApi.DataTransfer.CustomersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SimpleApi.DataTransfer.Validations
{
    public class CustomerDTOValidation : AbstractValidator<CustomerCreateUpdateDto>
    {
        public CustomerDTOValidation()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(100)
                .WithMessage("Incorrect name length. Name length must be from 10 to 100 chars.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(new Regex(@"^[0-9]{10}$"));

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
