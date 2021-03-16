using App.Core.Entity;
using AutoMapper;
using SimpleApi.DataTransfer.CustomersDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.MappingProfiles
{
    public class CustomerCreateDTOProfile : Profile
    {
        public CustomerCreateDTOProfile()
        {
            CreateMap<Customer, CustomerCreateUpdateDto>();
        }
    }
}
