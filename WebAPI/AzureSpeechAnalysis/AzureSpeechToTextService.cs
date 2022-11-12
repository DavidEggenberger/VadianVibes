using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebAPI.AzureSpeechAnalysis
{
    public class AzureSpeechToTextService
    {
        private readonly AzureSpeechAnalysisOptions options;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AzureSpeechToTextService(IOptions<AzureSpeechAnalysisOptions> options, IWebHostEnvironment webHostEnvironment)
        {
            this.options = options.Value;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> AnalyzeFormFile(IFormFile file)
        {
            var speechConfig = SpeechConfig.FromSubscription(options.APIKey, "switzerlandnorth");
            
            var id = Guid.NewGuid();
            var generatedFileName = $@"{webHostEnvironment.WebRootPath}/{id}.wav";

            using (var fileStream = file.OpenReadStream())
            {
                using (var destinationStream = new FileStream(generatedFileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    await fileStream.CopyToAsync(destinationStream);
                }
            };

            SpeechRecognitionResult speechRecognitionResult;
            using (var audioConfig = AudioConfig.FromWavFileInput(generatedFileName))
            {
                using (var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig))
                {
                    speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
                };
            };

            if (File.Exists(generatedFileName))
            {
                File.Delete(generatedFileName);
            }

            return speechRecognitionResult.Text;
        }
    }
}
