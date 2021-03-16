using App.Core.Entity;
using App.DataAccess.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleApi.DataTransfer.ItemsDto;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IRepository<Item> itemRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ItemController> logger;

        public ItemController(
            IRepository<Item> repository,
            IMapper mapper,
            ILogger<ItemController> logger)
        {
            this.itemRepository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> Create([FromBody] ItemCreateUpdateDto itemDTO)
        {
            try
            {
                if (itemDTO == null)
                {
                    return BadRequest();
                }

                var itemDomain = this.mapper.Map<Item>(itemDTO);
                await this.itemRepository.CreateAsync(itemDomain);
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
        public async Task<IActionResult> Update([FromRoute][Range(1, int.MaxValue)][Required] int id, [FromBody] ItemCreateUpdateDto itemDTO)
        {
            try
            {
                var item = await this.itemRepository.GetByIdWithIncludesAsync(id, false);

                if (item == null)
                {
                    return NotFound();
                }

                item.Cost = itemDTO.Cost;
                item.Description = itemDTO.Description;
                item.Manufacturer = itemDTO.Manufacturer;
                item.Name = itemDTO.Name;
                item.Nds = itemDTO.Nds;
                item.Refrigerate = itemDTO.Refrigerate;
                await this.itemRepository.UpdateAsync(item);

                return Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        [SwaggerResponse(200, Type = typeof(ItemDto))]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> Get([FromRoute][Range(1, int.MaxValue)][Required] int id)
        {
            try
            {
                var itemFromDB = await this.itemRepository.GetByIdWithIncludesAsync(id);

                if (itemFromDB == null)
                {
                    return NotFound();
                }

                var customerDTO = this.mapper.Map<ItemDto>(itemFromDB);
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
