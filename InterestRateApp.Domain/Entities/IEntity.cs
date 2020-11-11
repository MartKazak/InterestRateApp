using System;

namespace InterestRateApp.Domain.Entities
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}
