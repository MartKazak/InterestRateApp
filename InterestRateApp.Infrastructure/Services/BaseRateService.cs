using System;
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

        public async Task<decimal> GetBaseRateValue(string baseRateCode)
        {
            var response = await _httpClient.GetAsync($"getLatestVilibRate?RateType={baseRateCode}");
            var content = await response.Content.ReadAsStringAsync();
            var xmlDocument = ParseContentToXmlDocument(content);
            var xmlDocumentRoot = xmlDocument.Root;

            if (xmlDocumentRoot == null)
            {
                throw new ArgumentNullException(nameof(xmlDocumentRoot));
            }

            if (decimal.TryParse(xmlDocumentRoot.Value, out var baseRateValue))
            {
                return baseRateValue;
            }

            throw new ArgumentException("XML document root value can't be parsed to decimal");
        }

        private static XDocument ParseContentToXmlDocument(string content)
        {
            try
            {
                return XDocument.Parse(content);
            }
            catch (Exception e)
            {
               throw new ApplicationException("Content can't be parsed to XML document", e);
            }
        }
    }
}
