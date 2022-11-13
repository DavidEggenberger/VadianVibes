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
        private readonly AzureSpeechAnalysisAPIClient azureSpeechAnalysisAPIClient;

        public AzureSpeechAnalysisController(AzureSpeechAnalysisAPIClient azureSpeechAnalysisAPIClient)
        {
            this.azureSpeechAnalysisAPIClient = azureSpeechAnalysisAPIClient;
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<AzureCognitiveServicesTokenDTO> GetTokenAsync()
        {
            var token = await azureSpeechAnalysisAPIClient.GetTokenAsync();
            return new AzureCognitiveServicesTokenDTO
            {
                Token = token,
            };
        }

        [HttpGet("translate")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<TranslatedResult> GetAudioFile(string translatedText, [FromServices] TextToSpeechService textToSpeechService)
        {
            var text = await textToSpeechService.SynthesizeAudioAsync(translatedText);
            return new TranslatedResult { Address = text };
        }
    }
}
