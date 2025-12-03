using System.Net;
using System.Text;
using ESignatureService.Common;
using ESignatureService.Entities;
using ESignatureService.Interfaces.Services;
using Newtonsoft.Json.Linq;

namespace ESignatureService.Services;

public class EcpService(IHttpClientService httpClientService) : IECPService
{
    public string CreateJson(ESignaturePublicCertificate eSignaturePublicCertificate)
    {
        JObject certificateJson = JObject.FromObject(new
        {
            serial = eSignaturePublicCertificate.CertificateSerialNumber,
            version = eSignaturePublicCertificate.DocumentVersion.ToString(),
            begDT = eSignaturePublicCertificate.BeginCertificate.ToString("yyyy-MM-ddTHH:mm:ss"),
            name = eSignaturePublicCertificate.CertificateName,
            openKey = eSignaturePublicCertificate.PublicKey
        });
        string ogrnValue = ESignaturePublicCertificate.GetOGRN(eSignaturePublicCertificate.Subject!);
        if (!string.IsNullOrWhiteSpace(ogrnValue))
        {
            certificateJson["ogrn"] = ogrnValue;
        }
        return certificateJson.ToString();
    }

    public async Task<HttpResponseMessage> SendRequestCertificateId(string jsonCertificate, string emdUrl)
    {
        if (string.IsNullOrEmpty(emdUrl))
        {
            throw new ArgumentNullException($"{Constant.NULL_REFERENCE_EXCEPTION} {emdUrl}");
        }
        var content = new StringContent(jsonCertificate, Encoding.UTF8, "application/json");
        HttpResponseMessage httpResponseMessage = await httpClientService.SendRequestAsync(HttpMethod.Post, emdUrl, content);

            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
                throw new Exception(
                    $"Запрос по адресу {emdUrl} завершился с ошибкой: Status {httpResponseMessage.StatusCode}, Content:{httpResponseMessage.Content.ReadAsStringAsync()}");

        return httpResponseMessage;
    }
}