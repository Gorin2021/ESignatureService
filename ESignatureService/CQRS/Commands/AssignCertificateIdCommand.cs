using ESignatureService.Interfaces.CQRS.Commands;

namespace ESignatureService.CQRS.Commands
{
    public class AssignCertificateIdCommand:ICommand
    {
        public int ESignatureCertificateId { get; set; }
        public int UserId { get; set; }
    }
}
