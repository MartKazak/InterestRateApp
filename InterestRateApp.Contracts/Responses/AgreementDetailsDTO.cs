namespace InterestRateApp.Contracts.Responses
{
    public class AgreementDetailsDTO
    {
        public AgreementDTO Agreement { get; set; }
        public CustomerDTO Customer { get; set; }
        public decimal CurrentInterestRate { get; set; }
        public decimal NewInterestRate { get; set; }
        public decimal InterestRatesDifference { get; set; }
    }
}