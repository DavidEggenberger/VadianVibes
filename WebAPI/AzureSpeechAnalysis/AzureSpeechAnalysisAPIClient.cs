using System.Net.Http;
using System.Threading.Tasks;

namespace WebAPI.AzureSpeechAnalysis
{
    public class AzureSpeechAnalysisAPIClient
    {
        private readonly HttpClient httpClient;
        public AzureSpeechAnalysisAPIClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetToken()
        {
            var respnse = await httpClient.PostAsync("issuetoken", null);
            return null;
        }
    }
}
