using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InterestRateApp.Infrastructure.Services
{
    public class BaseRateService : IBaseRateService
    {
        private readonly HttpClient _httpClient;

        public BaseRateService(HttpClient httpClient) =>
            _httpClient = httpClient;

        public async Task<decimal> GetBaseRateValue(BaseRateCode baseRateCode)
        {
            var response = await _httpClient.GetAsync($"getLatestVilibRate?RateType={baseRateCode}");
            var content = await response.Content.ReadAsStringAsync();
            var xmlDocument = XDocument.Parse(content);

            return decimal.Parse(xmlDocument.Root.Value);
        }
    }
}
