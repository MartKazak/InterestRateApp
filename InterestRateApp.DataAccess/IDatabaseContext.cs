using System.Threading;
using System.Threading.Tasks;
using InterestRateApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterestRateApp.DataAccess
{
    public interface IDatabaseContext
    {
        DbSet<CustomerEntity> Customers { get; set; }
        DbSet<AgreementEntity> Agreements { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}