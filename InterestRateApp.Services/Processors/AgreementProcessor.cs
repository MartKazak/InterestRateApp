using System.Threading.Tasks;
using EnsureThat;
using InterestRateApp.Contracts.Requests;
using InterestRateApp.Contracts.Responses;
using InterestRateApp.Infrastructure;
using InterestRateApp.Infrastructure.Services;
using InterestRateApp.Services.Services;

namespace InterestRateApp.Services.Processors
{
    public class AgreementProcessor : IAgreementProcessor
    {
        private readonly IAgreementService _agreementService;
        private readonly IBaseRateService _baseRateService;
        private readonly ICustomerService _customerService;

        public AgreementProcessor(
            IAgreementService agreementService,
            ICustomerService customerService,
            IBaseRateService baseRateService)
        {
            _agreementService = agreementService;
            _customerService = customerService;
            _baseRateService = baseRateService;
        }

        public async Task<AgreementDetailsDTO> ProcessAgreementAsync(AgreementRequest agreementRequest)
        {
            Validate(agreementRequest);

            var agreementId = agreementRequest.Id;
            var existingAgreement = _agreementService.AgreementExists(agreementId);

            var currentBaseRateCode = existingAgreement
                ? await _agreementService.GetBaseRateCodeAsync(agreementId)
                : agreementRequest.BaseRateCode;

            var (currentBaseRateValue, newBaseRateValue) = 
                await GetBaseRateValuesAsync(currentBaseRateCode, agreementRequest.BaseRateCode);
            
            var agreement = _agreementService.AddOrUpdateAgreement(agreementRequest);
            var customer = await _customerService.GetCustomerAsync(agreementRequest.CustomerId);

            var margin = agreementRequest.Margin; 
            var currentInterestRate = GetInterestRate(currentBaseRateValue, margin);
            var newInterestRate = GetInterestRate(newBaseRateValue, margin);
            var interestRatesDifference = GetInterestRatesDifferent(currentInterestRate, newInterestRate);

            return new AgreementDetailsDTO
            {
                Agreement = agreement,
                Customer = customer,
                CurrentInterestRate = currentInterestRate,
                NewInterestRate = newInterestRate,
                InterestRatesDifference = interestRatesDifference
            };
        }

        private async Task<(decimal, decimal)> GetBaseRateValuesAsync(BaseRateCode currentBaseRateCode, BaseRateCode newBaseRateCode)
        {
            var newBaseRateValue = await _baseRateService.GetBaseRateValue(newBaseRateCode);
            var currentBaseRateValue = currentBaseRateCode == newBaseRateCode
                ? newBaseRateValue
                : await _baseRateService.GetBaseRateValue(currentBaseRateCode);

            return (currentBaseRateValue, newBaseRateValue);
        }

        private static decimal GetInterestRate(decimal baseRateValue, decimal margin) =>
            baseRateValue + margin;

        private static decimal GetInterestRatesDifferent(decimal currentRateValue, decimal newRateValue) =>
            currentRateValue - newRateValue;

        private static void Validate(AgreementRequest request)
        {
            Ensure.Guid.IsNotEmpty(request.Id, nameof(request.Id));
            EnsureArg.IsGt(request.Amount, 0, nameof(request.Amount));
            Ensure.Enum.IsDefined(request.BaseRateCode, nameof(request.BaseRateCode));
            EnsureArg.IsGt(request.Duration, 0, nameof(request.Duration));
            EnsureArg.IsGt(request.Margin, 0, nameof(request.Margin));
            Ensure.Guid.IsNotEmpty(request.CustomerId, nameof(request.CustomerId));
        }
    }
}
