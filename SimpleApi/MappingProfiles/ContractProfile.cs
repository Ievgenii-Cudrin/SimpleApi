using App.Core.Entity;
using AutoMapper;
using SimpleApi.DataTransfer.ContractsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.MappingProfiles
{
    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMap<ContractDto, Contract>();
            CreateMap<ContractCreateUpdateDto, Contract>();
        }
    }
}
