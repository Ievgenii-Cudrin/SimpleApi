using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SimpleApi.DataTransfer.ContractsDto;
using SimpleApi.DataTransfer.CustomersDto;
using SimpleApi.DataTransfer.DeliveriesDto;
using SimpleApi.DataTransfer.ItemsDto;
using SimpleApi.DataTransfer.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.DependencyInjection
{
    public static class ApiDependencyInjection
    {
        public static void InstallApiDependencyInjections(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CustomerDto>, CustomerDTOValidation>();
            services.AddTransient<IValidator<ItemDto>, ItemDTOValidation>();
            services.AddTransient<IValidator<DeliveryDto>, DeliveryDTOValidation>();
            services.AddTransient<IValidator<ContractCreateUpdateDto>, ContractCreateDTOValidation>();
        }
    }
}
