namespace ESignatureService.Interfaces.Services;

public interface IHttpClientService
{
    Task<HttpResponseMessage> SendRequestAsync(HttpMethod http, string uri, StringContent stringContent);
}