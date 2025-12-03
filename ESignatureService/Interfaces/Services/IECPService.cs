using ESignatureService.Entities;

namespace ESignatureService.Interfaces.Services
{
    public interface IECPService
    {
        string CreateJson(ESignaturePublicCertificate eSignaturePublicCertificate);
        Task<HttpResponseMessage> SendRequestCertificateId(string jsonCertificate, string emdUrl);
    }
}
