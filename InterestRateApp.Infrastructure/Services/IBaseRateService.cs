using System.Threading.Tasks;

namespace InterestRateApp.Infrastructure.Services
{
    public interface IBaseRateService
    {
        Task<decimal> GetBaseRateValue(string baseRateCode);
    }
}
