using App.Core.Entity;
using AutoMapper;
using SimpleApi.DataTransfer.ContractsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.MappingProfiles
{
    public class ContractDTOProfile : Profile
    {
        public ContractDTOProfile()
        {
            CreateMap<Contract, ContractDto>();
        }
    }
}
