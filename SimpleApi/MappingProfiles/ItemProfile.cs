using App.Core.Entity;
using AutoMapper;
using SimpleApi.DataTransfer.ItemsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.MappingProfiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemDto, Item>();
            CreateMap<ItemCreateUpdateDto, Item>();
        }
    }
}
