using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterestRateApp.Contracts.Responses;
using InterestRateApp.Services.Mappers;
using InterestRateApp.Services.Repositories;

namespace InterestRateApp.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerDTO>> GetCustomersAsync() => 
            (await _customerRepository.GetCustomersAsync())
                .Select(c => c.ToDTO());

        public async Task<CustomerDTO> GetCustomerAsync(Guid id) => 
            (await _customerRepository.GetCustomer(id)).ToDTO();
    }
}
