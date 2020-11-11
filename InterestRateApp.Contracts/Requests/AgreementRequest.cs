using System;
using InterestRateApp.Infrastructure;

namespace InterestRateApp.Contracts.Requests
{
    public class AgreementRequest
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public BaseRateCode BaseRateCode { get; set; }
        public int Duration { get; set; }
        public decimal Margin { get; set; }
        public Guid CustomerId { get; set; }
    }
}