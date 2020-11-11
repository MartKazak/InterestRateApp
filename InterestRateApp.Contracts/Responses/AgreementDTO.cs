using System;

namespace InterestRateApp.Contracts.Responses
{
    public class AgreementDTO
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public decimal Margin { get; set; }
        public string BaseRateCode { get; set; }
        public int Duration { get; set; }
    }
}