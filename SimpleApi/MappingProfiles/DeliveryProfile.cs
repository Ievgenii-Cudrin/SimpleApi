using App.Core.Entity;
using AutoMapper;
using SimpleApi.DataTransfer.DeliveriesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.MappingProfiles
{
    public class DeliveryProfile : Profile
    {
        public DeliveryProfile()
        {
            CreateMap<DeliveryDto, Delivery>();
            CreateMap<DeliveryCreateUpdateDto, Delivery>();
        }
    }
}
