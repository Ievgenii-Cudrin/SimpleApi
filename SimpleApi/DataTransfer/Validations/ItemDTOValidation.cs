using FluentValidation;
using SimpleApi.DataTransfer.ItemsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.DataTransfer.Validations
{
    public class ItemDTOValidation : AbstractValidator<ItemCreateUpdateDto>
    {
        public ItemDTOValidation()
        {
            RuleFor(x => x.Cost)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

            RuleFor(x => x.Manufacturer)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(80);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(x => x.Nds)
                .NotEmpty();

            RuleFor(x => x.Refrigerate)
                .NotEmpty();
        }
    }
}
