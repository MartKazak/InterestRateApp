using System;
using System.Threading.Tasks;
using InterestRateApp.Domain.Entities;
using InterestRateApp.Domain.Models;
using InterestRateApp.Infrastructure;

namespace InterestRateApp.Services.Repositories
{
    public interface IAgreementRepository
    {
        Task<BaseRateCode> GetBaseRateCode(Guid agreementId);
        void AddAgreement(AgreementEntity agreementEntity);
        Agreement AddOrUpdateAgreement(AgreementEntity agreementEntity);
        void UpdateAgreement(AgreementEntity agreementEntity);
        bool Exists(Guid agreementId);
    }
}