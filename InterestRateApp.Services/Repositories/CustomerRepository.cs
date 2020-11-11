using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterestRateApp.DataAccess;
using InterestRateApp.Domain.Entities;
using InterestRateApp.Domain.Models;
using InterestRateApp.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace InterestRateApp.Services.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public CustomerRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync() => 
            (await _databaseContext.Customers.ToListAsync())
                .Select(c => c.ToDomain());

        public async Task<Customer> GetCustomer(Guid id) => 
             (await _databaseContext.Customers.FindAsync(id)).ToDomain();

        public void AddCustomer(CustomerEntity customerEntity)
        {
            _databaseContext.Customers.Add(customerEntity);
            _databaseContext.SaveChanges();
        }

        public void AddOrUpdateCustomer(CustomerEntity customerEntity)
        {
            var customerId = customerEntity.Id;
            var existingCustomer = _databaseContext.Customers.Any(customer => customer.Id == customerId);

            if (existingCustomer)
            {
                UpdateCustomer(customerEntity);
            }
            else
            {
                AddCustomer(customerEntity);
            }
        }

        public void UpdateCustomer(CustomerEntity customerEntity)
        {
            _databaseContext.Customers.Update(customerEntity);
            _databaseContext.SaveChanges();
        }
    }
}