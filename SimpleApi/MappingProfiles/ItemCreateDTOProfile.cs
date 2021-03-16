using App.Core.Entity;
using AutoMapper;
using SimpleApi.DataTransfer.ItemsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.MappingProfiles
{
    public class ItemCreateDTOProfile : Profile
    {
        public ItemCreateDTOProfile()
        {
            CreateMap<Item, ItemCreateUpdateDto>();
        }
    }
}
