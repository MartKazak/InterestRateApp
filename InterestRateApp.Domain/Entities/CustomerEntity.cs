using System;
using System.Collections.Generic;

namespace InterestRateApp.Domain.Entities
{
    public class CustomerEntity : IEntity
    {
        public Guid Id { get; set; }
        public string PersonalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<AgreementEntity> Agreements { get; set; } = new List<AgreementEntity>();
    }
}
