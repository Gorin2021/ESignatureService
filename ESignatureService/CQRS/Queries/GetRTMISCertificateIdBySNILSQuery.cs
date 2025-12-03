using ESignatureService.Interfaces.CQRS.Queries;

namespace ESignatureService.CQRS.Queries
{
    public class GetRTMISCertificateIdBySNILSOrIdQuery : IQuery<long?>
    {
        public string? Snils { get; set; }
        public int? Id { get; set; }
    }
}