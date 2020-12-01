﻿using System;
using System.Threading.Tasks;
using InterestRateApp.Domain.Entities;
using InterestRateApp.Domain.Models;
using InterestRateApp.Infrastructure;

namespace InterestRateApp.Services.Repositories
{
    public interface IAgreementRepository
    {
        Task<BaseRateCode> GetBaseRateCode(Guid agreementId);
        Task<Agreement> AddAgreementAsync(AgreementEntity agreement);
        Task<Agreement> UpdateAgreementAsync(AgreementEntity agreement);
        bool Exists(Guid agreementId);
    }
}