using System;
using System.Reflection;
using InterestRateApp.Domain;
using InterestRateApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InterestRateApp.DataAccess
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<AgreementEntity> Agreements { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>(x =>
            {
                x.HasKey(k => k.Id);
                x.HasIndex(k => k.PersonalId).IsUnique();
                x.Property(p => p.PersonalId).HasMaxLength(11);
                x.Property(p => p.FirstName).IsRequired();
                x.Property(p => p.LastName).IsRequired();
            });

            modelBuilder.Entity<AgreementEntity>(x =>
            {
                x.HasKey(k => k.Id);
                x.Property(p => p.Amount).IsRequired();
                x.Property(p => p.BaseRateCode)
                    .HasMaxLength(9)
                    .HasConversion(
                    v => v.ToString(),
                    v => (BaseRateCode)Enum.Parse(typeof(BaseRateCode), v))
                    .IsRequired();
                x.Property(p => p.Duration).IsRequired();
                x.Property(p => p.Margin).IsRequired();
                x.HasOne(p => p.Customer)
                    .WithMany(p => p.Agreements)
                    .HasForeignKey(p => p.CustomerId);
            });

            SeedInitialData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void SeedInitialData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>()
               .HasData(
                   new CustomerEntity
                   {
                       Id = Guid.Parse("C7E7D3B9-1F25-4609-BBEC-2FA1B7564561"),
                       FirstName = "Peter",
                       LastName = "Peterson",
                       PersonalId = "12345678901",
                   },
                   new CustomerEntity
                   {
                       Id = Guid.Parse("F5E43D8A-16E8-4CEF-BA88-0AE3288AA7F7"),
                       FirstName = "Robert",
                       LastName = "Robertson",
                       PersonalId = "01987654321",
                   }
               );

            modelBuilder.Entity<AgreementEntity>()
                .HasData(
                    new AgreementEntity
                    {
                        Id = Guid.Parse("7C25E824-2582-497C-8C92-0D39DA9F74BF"),
                        Amount = 12000,
                        BaseRateCode = BaseRateCode.VILIBOR1m,
                        Duration = 60,
                        Margin = 1.6m,
                        CustomerId = Guid.Parse("C7E7D3B9-1F25-4609-BBEC-2FA1B7564561")
                    },
                    new AgreementEntity
                    {
                        Id = Guid.Parse("A686E94A-81DA-4990-8A83-CAA45358896D"),
                        Amount = 8000,
                        BaseRateCode = BaseRateCode.VILIBOR3m,
                        Duration = 36,
                        Margin = 2.2m,
                        CustomerId = Guid.Parse("F5E43D8A-16E8-4CEF-BA88-0AE3288AA7F7")
                    },
                    new AgreementEntity
                    {
                        Id = Guid.Parse("E3F20C0F-A742-4684-ACE1-DA3470C4C990"),
                        Amount = 8000,
                        BaseRateCode = BaseRateCode.VILIBOR1y,
                        Duration = 24,
                        Margin = 1.85m,
                        CustomerId = Guid.Parse("F5E43D8A-16E8-4CEF-BA88-0AE3288AA7F7")
                    }
                );
        }
    }

    // TODO: Create extension to configure DbContext
    // TODO: use configuration builder to get connection string
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.UseSqlServer("data source=.; initial catalog=InterestRateApp; integrated security=true",
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(DatabaseContext).GetTypeInfo().Assembly.GetName().Name));

            return new DatabaseContext(builder.Options);
        }
    }
}
