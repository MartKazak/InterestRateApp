using System;
using System.Threading.Tasks;
using InterestRateApp.Contracts.Requests;
using InterestRateApp.Contracts.Responses;
using InterestRateApp.Infrastructure;
using InterestRateApp.Services.Mappers;
using InterestRateApp.Services.Repositories;

namespace InterestRateApp.Services.Services
{
    public class AgreementService : IAgreementService
    {
        private readonly IAgreementRepository _agreementRepository;

        public AgreementService(IAgreementRepository agreementRepository)
        {
            _agreementRepository = agreementRepository;
        }

        public bool AgreementExists(Guid agreementId) => 
            _agreementRepository.Exists(agreementId);

        public Task<BaseRateCode> GetBaseRateCodeAsync(Guid agreementId) => 
            _agreementRepository.GetBaseRateCode(agreementId);

        public AgreementDTO AddOrUpdateAgreement(AgreementRequest agreementRequest) => 
            _agreementRepository.AddOrUpdateAgreement(agreementRequest.ToEntity()).ToDTO();
    }
}
