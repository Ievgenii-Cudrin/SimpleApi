using FluentValidation;
using SimpleApi.DataTransfer.DeliveriesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.DataTransfer.Validations
{
    public class DeliveryDTOValidation : AbstractValidator<DeliveryCreateUpdateDto>
    {
        public DeliveryDTOValidation()
        {
            RuleFor(x => x.Address)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

            RuleFor(x => x.TypeDelivery)
                .IsInEnum();
        }
    }
}
