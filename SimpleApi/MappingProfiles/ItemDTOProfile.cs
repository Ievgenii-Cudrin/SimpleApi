using App.Core.Entity;
using AutoMapper;
using SimpleApi.DataTransfer.ItemsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.MappingProfiles
{
    public class ItemDTOProfile : Profile
    {
        public ItemDTOProfile()
        {
            CreateMap<Item, ItemDto>();
        }
    }
}
