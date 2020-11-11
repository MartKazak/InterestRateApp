using System;

namespace InterestRateApp.Contracts.Responses
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }
        public string PersonalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}