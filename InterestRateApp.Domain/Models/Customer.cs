using System;

namespace InterestRateApp.Domain.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string PersonalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}