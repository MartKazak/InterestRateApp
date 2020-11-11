using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InterestRateApp.Domain.Entities;
using InterestRateApp.Domain.Models;

namespace InterestRateApp.Services.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomer(Guid id);
        void AddCustomer(CustomerEntity customerEntity);
        void AddOrUpdateCustomer(CustomerEntity customerEntity);
        void UpdateCustomer(CustomerEntity customerEntity);
    }
}
