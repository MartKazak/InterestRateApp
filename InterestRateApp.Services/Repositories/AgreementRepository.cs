using System;
using System.Linq;
using System.Threading.Tasks;
using InterestRateApp.DataAccess;
using InterestRateApp.Domain;
using InterestRateApp.Domain.Entities;
using InterestRateApp.Domain.Models;
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

        public async Task<BaseRateCode> GetBaseRateCode(Guid agreementId) =>
            await _databaseContext.Agreements
                .Where(agreement => agreement.Id == agreementId)
                .Select(agreement => agreement.BaseRateCode)
                .FirstOrDefaultAsync();

        public async Task<Agreement> AddAgreementAsync(AgreementEntity agreementEntity)
        {
            await _databaseContext.Agreements.AddAsync(agreementEntity);
            await _databaseContext.SaveChangesAsync();
            return agreementEntity.ToDomain();
        }

        public async Task<Agreement> UpdateAgreementAsync(AgreementEntity agreementEntity)
        {
            var agreementId = agreementEntity.Id;
            var currentAgreement = await _databaseContext.Agreements.FindAsync(agreementId);

            if (currentAgreement == null)
            {
                throw new ArgumentNullException(nameof(agreementEntity), $"Agreement entity with Id: {agreementId} was not found");
            }

            currentAgreement.Amount = agreementEntity.Amount;
            currentAgreement.BaseRateCode = agreementEntity.BaseRateCode;
            currentAgreement.CustomerId = agreementEntity.CustomerId;
            currentAgreement.Duration = currentAgreement.Duration;

            _databaseContext.Agreements.Update(currentAgreement);
            await _databaseContext.SaveChangesAsync();

            return currentAgreement.ToDomain();
        }

        public bool Exists(Guid agreementId) => 
            _databaseContext.Agreements.Any(agreement => agreement.Id == agreementId);
    }
}
