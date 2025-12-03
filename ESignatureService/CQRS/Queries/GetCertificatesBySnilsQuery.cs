using ESignatureService.Interfaces.CQRS.Queries;
using ESignatureService.Models;

namespace ESignatureService.CQRS.Queries
{
    public class GetCertificatesBySnilsQuery:IQuery<IEnumerable<PublicCertificateViewModel>>
    {
        public required string Snils { get; set; }
        public bool? IsValid { get; set; }
    }
}
