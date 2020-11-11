using InterestRateApp.Contracts.Responses;
using InterestRateApp.Domain.Entities;
using InterestRateApp.Domain.Models;

namespace InterestRateApp.Services.Mappers
{
    public static class CustomerMapper
    {
        public static Customer ToDomain(this CustomerEntity customerEntity)
        {
            return new Customer
            {
                Id = customerEntity.Id,
                FirstName = customerEntity.FirstName,
                LastName = customerEntity.LastName,
                PersonalId = customerEntity.PersonalId
            };
        }

        public static CustomerDTO ToDTO(this Customer customer)
        {
            return new CustomerDTO
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PersonalId = customer.PersonalId
            };
        }
    }
}
