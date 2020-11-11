using System.Threading.Tasks;
using InterestRateApp.Contracts.Requests;
using InterestRateApp.Contracts.Responses;
using InterestRateApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace InterestRateApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgreementsController : ControllerBase
    {
        private readonly IAgreementProcessor _agreementProcessor;

        public AgreementsController(IAgreementProcessor agreementProcessor) => 
            _agreementProcessor = agreementProcessor;

        [HttpPost]
        public async Task<AgreementDetailsDTO> Post([FromBody] AgreementRequest request) =>
            await _agreementProcessor.ProcessAgreementAsync(request);
    }
}
