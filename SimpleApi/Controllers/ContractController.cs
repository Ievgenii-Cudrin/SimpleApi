using App.Core.Entity;
using App.DataAccess.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleApi.DataTransfer.ContractsDto;
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
    public class ContractController : ControllerBase
    {
        private readonly IRepository<Contract> contractRepository;
        private readonly IRepository<Item> itemRepositry;
        private readonly IRepository<Delivery> deliveryRepository;
        private readonly IRepository<Customer> customerRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ContractController> logger;

        public ContractController(
            IRepository<Contract> contractRepository,
            IRepository<Item> itemRepository,
            IRepository<Delivery> deliveryRepository,
            IRepository<Customer> customerRepository,
            IMapper mapper,
            ILogger<ContractController> logger)
        {
            this.contractRepository = contractRepository;
            this.itemRepositry = itemRepository;
            this.deliveryRepository = deliveryRepository;
            this.customerRepository = customerRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> Create([FromBody] ContractCreateUpdateDto contractDTO)
        {
            try
            {
                if (contractDTO == null)
                {
                    return BadRequest();
                }

                var delivery = await this.deliveryRepository.GetByIdWithIncludesAsync(contractDTO.DeliveryId);
                var customer = await this.customerRepository.GetByIdWithIncludesAsync(contractDTO.CustomerId);
                var items = await this.itemRepositry.FindAsync(t => contractDTO.ItemId.Contains(t.Id));

                if (delivery == null || customer == null || items.Count() == 0)
                {
                    return NotFound();
                }

                var contractDomain = this.mapper.Map<Contract>(contractDTO);
                await this.contractRepository.CreateAsync(contractDomain);
                contractDomain.Items = items;

                await this.contractRepository.UpdateAsync(contractDomain);

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
        public async Task<IActionResult> Update([FromRoute][Range(1, int.MaxValue)][Required] int id, [FromBody] ContractCreateUpdateDto contractDTO)
        {
            try
            {
                var contracts = await this.contractRepository.FindAsync(x => x.Id == id);
                var contract = contracts.FirstOrDefault();

                if (contract == null)
                {
                    return NotFound();
                }

                var delivery = await this.deliveryRepository.GetByIdWithIncludesAsync(contractDTO.DeliveryId);
                var customer = await this.customerRepository.GetByIdWithIncludesAsync(contractDTO.CustomerId);
                var items = await this.itemRepositry.FindAsync(t => contractDTO.ItemId.Contains(t.Id));

                if (delivery == null || customer == null || items.Count() == 0)
                {
                    return NotFound();
                }

                contract = this.mapper.Map<Contract>(contractDTO);
                contract.Id = id;
                contract.Items = items;

                await this.contractRepository.UpdateAsync(contract);

                return Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        [SwaggerResponse(200, Type = typeof(Contract))]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> Get([FromRoute][Range(1, int.MaxValue)][Required] int id)
        {
            try
            {
                var contract = await this.contractRepository.GetByIdWithIncludesAsync(id, true, x => x.Customer, x => x.Delivery);

                if (contract == null)
                {
                    return NotFound();
                }

                return Ok(contract);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
