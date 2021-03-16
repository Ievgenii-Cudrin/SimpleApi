using FluentValidation;
using SimpleApi.DataTransfer.ContractsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.DataTransfer.Validations
{
    public class ContractCreateDTOValidation : AbstractValidator<ContractCreateUpdateDto>
    {
        public ContractCreateDTOValidation()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty();

            RuleFor(x => x.DeliveryId)
                .NotEmpty();
        }
    }
}
