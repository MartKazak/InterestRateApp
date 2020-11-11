using System;
using System.Threading.Tasks;
using InterestRateApp.Contracts.Requests;
using InterestRateApp.Contracts.Responses;
using InterestRateApp.Infrastructure;

namespace InterestRateApp.Services.Services
{
    public interface IAgreementService
    {
        bool AgreementExists(Guid agreementId);
        Task<BaseRateCode> GetBaseRateCodeAsync(Guid agreementId);
        AgreementDTO AddOrUpdateAgreement(AgreementRequest agreementRequest);
    }
}
