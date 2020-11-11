using System;
using System.Linq;
using System.Threading.Tasks;
using InterestRateApp.DataAccess;
using InterestRateApp.Domain.Entities;
using InterestRateApp.Domain.Models;
using InterestRateApp.Infrastructure;
using InterestRateApp.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace InterestRateApp.Services.Repositories
{
    public class AgreementRepository : IAgreementRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public AgreementRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<BaseRateCode> GetBaseRateCode(Guid agreementId)
        {
            return await _databaseContext.Agreements
                .Where(agreement => agreement.Id == agreementId)
                .Select(agreement => agreement.BaseRateCode)
                .FirstOrDefaultAsync();
        }

        public void AddAgreement(AgreementEntity agreementEntity)
        {
            _databaseContext.Agreements.Add(agreementEntity);
            _databaseContext.SaveChanges();
        }

        public Agreement AddOrUpdateAgreement(AgreementEntity agreementEntity)
        {
            var agreementId = agreementEntity.Id;
            var existingAgreement = Exists(agreementId);

            if (existingAgreement)
            {
                UpdateAgreement(agreementEntity);
            }
            else
            {
                AddAgreement(agreementEntity);
            }

            return agreementEntity.ToDomain();
        }

        public void UpdateAgreement(AgreementEntity agreementEntity)
        {
            var agreementId = agreementEntity.Id;
            var currentAgreement = _databaseContext.Agreements.Find(agreementId);

            if (currentAgreement == null)
            {
                throw new ArgumentNullException(nameof(agreementEntity), $"Agreement entity with Id: {agreementId} was not found");
            }

            currentAgreement.Amount = agreementEntity.Amount;
            currentAgreement.BaseRateCode = agreementEntity.BaseRateCode;
            currentAgreement.CustomerId = agreementEntity.CustomerId;
            currentAgreement.Duration = currentAgreement.Duration;

            _databaseContext.Agreements.Update(currentAgreement);
            _databaseContext.SaveChanges();
        }

        public bool Exists(Guid agreementId) => 
            _databaseContext.Agreements.Any(agreement => agreement.Id == agreementId);
    }
}
