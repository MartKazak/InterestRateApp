﻿using System;
using System.Threading.Tasks;
using InterestRateApp.Contracts.Requests;
using InterestRateApp.Contracts.Responses;
using InterestRateApp.Domain;

namespace InterestRateApp.Services.Services
{
    public interface IAgreementService
    {
        bool AgreementExists(Guid agreementId);
        Task<BaseRateCode> GetBaseRateCodeAsync(Guid agreementId);
        Task<AgreementDTO> AddAgreementAsync(AgreementRequest agreementRequest);
        Task<AgreementDTO> UpdateAgreementAsync(AgreementRequest agreementRequest);
    }
}
