using App.Core.Entity;
using App.DataAccess.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleApi.DataTransfer.CustomersDto;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<Customer> customerRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CustomerController> logger;

        public CustomerController(
            IRepository<Customer> repository,
            IMapper mapper,
            ILogger<CustomerController> logger)
        {
            this.customerRepository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> Create([FromBody] CustomerCreateUpdateDto customerDTO)
        {
            try
            {
                if (customerDTO == null)
                {
                    return BadRequest();
                }

                var customerDomain = this.mapper.Map<Customer>(customerDTO);
                await this.customerRepository.CreateAsync(customerDomain);
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
        public async Task<IActionResult> Update([FromRoute][Range(1, int.MaxValue)][Required] int id, [FromBody] CustomerCreateUpdateDto customerDTO)
        {
            try
            {
                var custumers = await this.customerRepository.FindAsync(x => x.Id == id);
                var customer = custumers.FirstOrDefault();

                if (customer == null)
                {
                    return NotFound();
                }

                customer = this.mapper.Map<Customer>(customerDTO);
                await this.customerRepository.UpdateAsync(customer);
                return Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        [SwaggerResponse(200, Type = typeof(CustomerDto))]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        public async Task<IActionResult> Get([FromRoute][Range(1, int.MaxValue)][Required] int id)
        {
            try
            {
                var customerFromDB = await this.customerRepository.GetByIdWithIncludesAsync(id);

                if (customerFromDB == null)
                {
                    return NotFound();
                }

                var customerDTO = this.mapper.Map<CustomerDto>(customerFromDB);
                return Ok(customerDTO);
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
