using System;

namespace InterestRateApp.Domain.Entities
{
    public class AgreementEntity : IEntity
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public decimal Margin { get; set; }
        public BaseRateCode BaseRateCode { get; set; }
        public int Duration { get; set; }

        public CustomerEntity Customer { get; set; }
        public Guid CustomerId { get; set; }
    }
}
