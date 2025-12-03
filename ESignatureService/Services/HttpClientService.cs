using ESignatureService.Interfaces.Services;

namespace ESignatureService.Services
{
    public class HttpClientService(IHttpClientFactory httpClientFactory) : IHttpClientService
    {
        public Task<HttpResponseMessage> SendRequestAsync(HttpMethod httpMethod, string uri, StringContent stringContent)
        {
            using var client = httpClientFactory.CreateClient();
            var requestMessage = new HttpRequestMessage(httpMethod, uri);
            requestMessage.Content = stringContent;
            // ReSharper disable once ReturnOfTaskProducedByUsingVariable
            return  client.SendAsync(requestMessage);
        }
    }
}
