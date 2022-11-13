using Microsoft.AspNetCore.Hosting;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace WebAPI.AzureSpeechAnalysis
{
    public class TextToSpeechService
    {
        private readonly AzureSpeechAnalysisOptions azureTranslatorOptions;
        IWebHostEnvironment env;
        public TextToSpeechService(IOptions<AzureSpeechAnalysisOptions> azureTranslatorOptions, IWebHostEnvironment env)
        {
            this.azureTranslatorOptions = azureTranslatorOptions.Value;
            this.env = env;
        }
        public async Task<string> SynthesizeAudioAsync(string text)
        {
            text = text.Replace('$', ' ');
            var config = SpeechConfig.FromSubscription(azureTranslatorOptions.APIKey, "switzerlandnorth");

            config.SpeechSynthesisLanguage = "de-DE";

            config.SpeechSynthesisVoiceName = "de-DE-ConradNeural";

            using var synthesizer = new SpeechSynthesizer(config);

            var result = await synthesizer.SpeakTextAsync(text);
            using var stream = AudioDataStream.FromResult(result);
            Guid id = Guid.NewGuid();
            try
            {
                await stream.SaveToWaveFileAsync($"{env.WebRootPath}/{id}.wav");

            }
            catch (Exception ex)
            {

            }
            return $"/{id}.wav";
        }
    }
}
