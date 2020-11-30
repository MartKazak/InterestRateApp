using System.Threading.Tasks;
using InterestRateApp.Contracts.Requests;
using InterestRateApp.Contracts.Responses;

namespace InterestRateApp.Services.Processors
{
    public interface IAgreementProcessor
    {
        Task<AgreementDetailsDTO> ProcessAgreementAsync(AgreementRequest agreementRequest);
    }
}