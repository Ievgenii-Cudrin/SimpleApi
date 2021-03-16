using App.Core.Entity;
using AutoMapper;
using SimpleApi.DataTransfer.DeliveriesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.MappingProfiles
{
    public class DeliveryDTOProfile : Profile
    {
        public DeliveryDTOProfile()
        {
            CreateMap<Delivery, DeliveryDto>();
        }
    }
}
