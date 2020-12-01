using System;

namespace InterestRateApp.Domain.Models
{
    public class Agreement
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public decimal Margin { get; set; }
        public BaseRateCode BaseRateCode { get; set; }
        public int Duration { get; set; }
    }
}
