using System;
using InterestRateApp.Contracts.Requests;
using InterestRateApp.Contracts.Responses;
using InterestRateApp.Domain.Entities;
using InterestRateApp.Domain.Models;
using InterestRateApp.Infrastructure;

namespace InterestRateApp.Services.Mappers
{
    public static class AgreementMapper
    {
        public static AgreementEntity ToEntity(this AgreementRequest request) =>
            new AgreementEntity
            {
                Id = request.Id,
                Amount = request.Amount,
                BaseRateCode = request.BaseRateCode,
                CustomerId = request.CustomerId,
                Duration = request.Duration,
                Margin = request.Margin
            };

        public static AgreementDTO ToDTO(this Agreement agreement) =>
            new AgreementDTO
            {
                Id = agreement.Id,
                Amount = agreement.Amount,
                BaseRateCode = Enum.GetName(typeof(BaseRateCode), agreement.BaseRateCode),
                Duration = agreement.Duration,
                Margin = agreement.Margin
            };

        public static Agreement ToDomain(this AgreementEntity entity) =>
            new Agreement
            {
                Id = entity.Id,
                Amount = entity.Amount,
                BaseRateCode = entity.BaseRateCode,
                Duration = entity.Duration,
                Margin = entity.Margin
            };
    }
}
