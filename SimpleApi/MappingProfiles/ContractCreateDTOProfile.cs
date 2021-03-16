using App.Core.Entity;
using AutoMapper;
using SimpleApi.DataTransfer.ContractsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.MappingProfiles
{
    public class ContractCreateDTOProfile : Profile
    {
        public ContractCreateDTOProfile()
        {
            CreateMap<Contract, ContractCreateUpdateDto>();
        }
    }
}
