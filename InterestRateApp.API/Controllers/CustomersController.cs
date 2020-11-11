using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InterestRateApp.Contracts.Responses;
using InterestRateApp.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace InterestRateApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService) => 
            _customerService = customerService;

        [HttpGet]
        public async Task<IEnumerable<CustomerDTO>> Get() => 
            await _customerService.GetCustomersAsync();
        
        [HttpGet("{id}")]
        public async Task<CustomerDTO> Get(Guid id) => 
            await _customerService.GetCustomerAsync(id);
    }
}
