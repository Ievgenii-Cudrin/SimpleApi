using App.Core.Entity;
using App.DataAccess.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleApi.DataTransfer.DeliveriesDto;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IRepository<Delivery> deliveryRepository;
        private readonly IMapper mapper;
        private readonly ILogger<DeliveryController> logger;

        public DeliveryController(
            IRepository<Delivery> repository,
            IMapper mapper,
            ILogger<DeliveryController> logger)
        {
            this.deliveryRepository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> Create([FromBody] DeliveryCreateUpdateDto deliveryDTO)
        {
            try
            {
                if (deliveryDTO == null)
                {
                    return BadRequest();
                }

                var deliveryDomain = this.mapper.Map<Delivery>(deliveryDTO);
                await this.deliveryRepository.CreateAsync(deliveryDomain);
                return Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("{id:int}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> Update([FromRoute][Range(1, int.MaxValue)][Required] int id, [FromBody] DeliveryCreateUpdateDto deliveryDTO)
        {
            try
            {
                var delivery = await this.deliveryRepository.GetByIdWithIncludesAsync(id, false);

                if (delivery == null)
                {
                    return NotFound();
                }

                delivery.Address = deliveryDTO.Address;
                delivery.TypeDelivery = (App.Core.Enumeration.DeliveryType)deliveryDTO.TypeDelivery;
                await this.deliveryRepository.UpdateAsync(delivery);

                return Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        [SwaggerResponse(200, Type = typeof(DeliveryDto))]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> Get([FromRoute][Range(1, int.MaxValue)][Required] int id)
        {
            try
            {
                var itemFromDB = await this.deliveryRepository.GetByIdWithIncludesAsync(id);

                if (itemFromDB == null)
                {
                    return NotFound();
                }

                var customerDTO = this.mapper.Map<DeliveryDto>(itemFromDB);
                return Ok(customerDTO);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
