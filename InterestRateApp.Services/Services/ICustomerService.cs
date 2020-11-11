using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InterestRateApp.Contracts.Responses;

namespace InterestRateApp.Services.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetCustomersAsync();
        Task<CustomerDTO> GetCustomerAsync(Guid id);
    }
}
