using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebAPI.AzureSpeechAnalysis;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureSpeechAnalysisController : ControllerBase
    {
        private readonly AzureSpeechAnalysisOptions azureSpeechOptions;

        public AzureSpeechAnalysisController(IOptions<AzureSpeechAnalysisOptions> azureSpeechOptions)
        {
            this.azureSpeechOptions = azureSpeechOptions.Value;
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<AzureCognitiveServicesTokenDTO> GetToken()
        {


            return new AzureCognitiveServicesTokenDTO
            {
                Token = string.Empty
            };
        }
    }
}
